using PoolingEngine.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolingEngine.Domain.Entities
{
    public class RequestPooling
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public IEnumerable<int> DeviceItemIds { get; set; } = new HashSet<int>();
        public IEnumerable<int>? TagGroupIds { get; set; }
        public EnumPoolingPriority Priority { get; set; } = EnumPoolingPriority.LOW;
    }
}
