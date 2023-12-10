using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoolingEngine.Models
{
    public class RequestItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public DeviceItem? Device { get; set; }
        public TagItem? Tag { get; set; }
        public EnumPoolingPriority Priority { get; set; }

    }
}
