using Opc.UaFx;
using PoolingEngine.Domain.Entities;
using PoolingEngine.Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolingEngine.Domain.Repository
{
    public interface IOpcUaRepository
    {
        IEnumerable<OpcValue>? OpcRead(DeviceItem deviceItem, List<TagDef> tagDefs);
        void OpcWrite(DeviceItem deviceItem, TagDef tagDef, WriteItem writeItem, EnumRequestType enumRequestType);
        void OpcCommand();
    }
}
