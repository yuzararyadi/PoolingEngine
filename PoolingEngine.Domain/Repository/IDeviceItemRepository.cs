using PoolingEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolingEngine.Domain.Repository
{
    public interface IDeviceItemRepository : IGenericRepository<DeviceItem>
    {
        void updatelinkTagGroups(DeviceItem deviceItem, List<TagGroup> tagGroups);
    }
}
