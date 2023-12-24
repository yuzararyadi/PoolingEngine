using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolingEngine.Domain.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IDeviceItemRepository DeviceItem { get; }
        IRequestItemRepository RequestItem { get; }
        ITagGroupRepository TagGroup { get; }
        ITagItemRepository TagItem { get; }
        ITagValueRepository TagValue { get; }
        int Save();

    }
}
