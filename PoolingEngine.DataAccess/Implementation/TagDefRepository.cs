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
    public class TagDefRepository : GenericRepository<TagDef>, ITagDefRepository
    {
        private readonly AppDbContext _dbcontext;
        public TagDefRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }
        public List<TagDef> ListDeviceTagDef(DeviceItem deviceItem, List<TagItem>tagItems)
        {
            var listTagDefs = _dbcontext.TagDefs.Where(x=> x.DeviceItem == deviceItem && tagItems.Contains(x.TagItem));
            return listTagDefs.ToList();
        }
    }
}
