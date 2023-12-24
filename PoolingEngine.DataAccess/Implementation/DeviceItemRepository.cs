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
    public class DeviceItemRepository : GenericRepository<DeviceItem>, IDeviceItemRepository
    {
        private  readonly AppDbContext _dbcontext;

        public DeviceItemRepository(AppDbContext context) : base(context) 
        {
            _dbcontext = context;
        }
    }
}
