using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolingEngine.Domain.Entities
{
    public class TagDef
    {
        public int Id { get; set; }
        public DeviceItem? DeviceItem { get; set; }
        public TagItem? TagItem { get; set; }
        public string MapAddress { get; set; } = string.Empty;

    }
}
