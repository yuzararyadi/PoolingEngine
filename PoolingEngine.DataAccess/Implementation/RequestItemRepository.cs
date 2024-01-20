using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Opc.UaFx;
using PoolingEngine.DataAccess.Context;
using PoolingEngine.Domain.Entities;
using PoolingEngine.Domain.Entities.Enum;
using PoolingEngine.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PoolingEngine.DataAccess.Implementation
{

    public class RequestItemRepository : IRequestItemRepository
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IOpcUaRepository _opcUaRepository;
        public RequestItemRepository(IServiceProvider serviceProvider,
            IOpcUaRepository opcUaRepository)
        {
            _serviceProvider = serviceProvider;
            _opcUaRepository = opcUaRepository;

        }
        private List<RequestItem> _requestItems = new List<RequestItem>();
        public List<RequestItem> RequestItems()
        {
            return _requestItems;
        }
        public IQueryable<RequestItem> GetAllwithChild(params Expression<Func<RequestItem, object>>[] includeExpressions)
        {
            IQueryable<RequestItem> set = (IQueryable<RequestItem>)_requestItems;
            foreach (var includeExpression in includeExpressions)
            {
                set = set.Include(includeExpression);
            }
            return set;
        }
        public void Add(RequestItem requestItem)
        {
            _requestItems.Add(requestItem);
        }

        public void Remove(RequestItem item)
        {
            _requestItems.Remove(item);
        }

        public IEnumerable<RequestItem> PopulateRequestItem(RequestPooling requestPooling, List<TagGroup> tagGroups)
        {
            List<RequestItem> requestItems = new List<RequestItem>();
            foreach (int deviceitemId in requestPooling.DeviceItemIds)
            {
                RequestItem requestItem = new RequestItem()
                {
                    RequestPoolingId = requestPooling.Id.ToString(),
                    DeviceItemId = deviceitemId,
                    TagGroups = tagGroups,
                    Priority = requestPooling.Priority
                };
                _requestItems.Add(requestItem);
                requestItems.Add(requestItem);
            }
            return requestItems;
        }

        public List<TagValue> ExecuteRequestItem()
        {   
            List<TagValue> tagValues = new List<TagValue>();
            var requestItem = RequestItems().ToList()
                       .OrderByDescending(x => x.Priority)
                       .ThenByDescending(x => x.TimeStamp).FirstOrDefault();
            if (requestItem != null)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<IUnitOfWork>();
                    var deviceItem = context.DeviceItem.GetById(requestItem.DeviceItemId);
                    if (deviceItem == null) return tagValues;
                    if (requestItem.RequestType == EnumRequestType.READ)
                    {
                        OpcReadOperation(context, requestItem, deviceItem,tagValues);
                    }
                    else
                    {
                        OpcWriteOperation(context, requestItem, deviceItem, tagValues);
                    }
                }
                Remove(requestItem);
            }
            return tagValues;
        }
        private void OpcReadOperation(IUnitOfWork context, 
            RequestItem requestItem, 
            DeviceItem deviceItem, 
            List<TagValue> tagValues)
        {
            var listTagItems = context.TagGroup.LinkedTagItems(requestItem.TagGroups.ToList());
            if (listTagItems.Count > 0 && deviceItem != null)
            {
                var tagDefs = context.TagDef.GetAllwithChild(x => x.DeviceItem, y => y.TagItem)
                    .Where(x => x.DeviceItem == deviceItem && listTagItems.Contains(x.TagItem)).ToList();
                var OpcValue = _opcUaRepository.OpcRead(deviceItem, tagDefs);
                if (OpcValue != null)
                {
                    int i = 0;
                    foreach (OpcValue value in OpcValue)
                    {
                        TagValue tagValue = new TagValue()
                        {
                            RequestItemId = requestItem.Id.ToString(),
                            DeviceItemId = requestItem.DeviceItemId,
                            TagItemId = tagDefs[i].TagItem.Id,
                            Value = value.Value.ToString(),
                            TimeStamp = (DateTime) value.SourceTimestamp
                        };
                        tagValues.Add(tagValue);
                        i++;
                    }
                }
            }
        }
        private void OpcWriteOperation(IUnitOfWork context,
            RequestItem requestItem,
            DeviceItem deviceItem,
            List<TagValue> tagValues)
        {
            var tagItem = context.TagItem.GetById(requestItem.WriteItem.TagItemId);
            if (tagItem == null) return;
            var tagDef = context.TagDef.GetAllwithChild(x => x.DeviceItem, y => y.TagItem)
                .Where(x => x.DeviceItem == deviceItem && x.TagItem == tagItem).FirstOrDefault();
            if (tagDef == null) return;
            _opcUaRepository.OpcWrite(deviceItem, tagDef, requestItem.WriteItem, requestItem.RequestType);
        }

    }
}
