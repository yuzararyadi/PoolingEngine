using PoolingEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PoolingEngine.Domain.Repository
{
    public interface IRequestItemRepository //: IGenericRepository<RequestItem>
    {
        //public IEnumerable<RequestItem> PopulateRequestItem(RequestPooling requestPooling, List<TagGroup> tagGroups);
        List<RequestItem> RequestItems();
        void Add(RequestItem item);
        void Remove(RequestItem item);
        //void SaveChanges();
        IQueryable<RequestItem> GetAllwithChild(params Expression<Func<RequestItem, object>>[] includeExpressions);
        IEnumerable<RequestItem> PopulateRequestItem(RequestPooling requestPooling, List<TagGroup> tagGroups);

    }
}
