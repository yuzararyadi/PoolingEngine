using Microsoft.EntityFrameworkCore;
using PoolingEngine.DataAccess.Context;
using PoolingEngine.Domain.Entities;
using PoolingEngine.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolingEngine.DataAccess.Implementation
{
    public class RequestItemRepository :  GenericInMemoryRepository<RequestItem>, IRequestItemRepository
    {
        private readonly InMemoryDbContext _inMemoryDbContext;

        public RequestItemRepository(InMemoryDbContext inMemoryDbContext) : base(inMemoryDbContext)
        {
            _inMemoryDbContext = inMemoryDbContext;
            
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
                _inMemoryDbContext.RequestItems.Add(requestItem);

                requestItems.Add(requestItem);
            }

            return requestItems;
        }
    }
}
