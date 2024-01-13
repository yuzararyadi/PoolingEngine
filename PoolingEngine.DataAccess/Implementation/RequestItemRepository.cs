using Microsoft.EntityFrameworkCore;
using PoolingEngine.DataAccess.Context;
using PoolingEngine.Domain.Entities;
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
        private List<RequestItem> _requestItems = new List<RequestItem>();
        public List<RequestItem> RequestItems()
        {
            return _requestItems;
        }
        //public IQueryable<RequestItem> RequestItems => _requestItems.AsQueryable();

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


        //private readonly InMemoryDbContext _inMemoryDbContext;

        //public RequestItemRepository(InMemoryDbContext inMemoryDbContext) : base(inMemoryDbContext)
        //{
        //    _inMemoryDbContext = inMemoryDbContext;

        //}

        public IEnumerable<RequestItem> PopulateRequestItem(RequestPooling requestPooling, List<TagGroup> tagGroups)
        {
            //_inMemoryDbContext.TagGroups.AddRange(tagGroups);
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
    }
}
