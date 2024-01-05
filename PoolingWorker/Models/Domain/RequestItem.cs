using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PoolingWorker.Enum;

namespace PoolingWorker.Models.Domain
{
    public class RequestItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public EnumPoolingPriority Priority { get; set; } = EnumPoolingPriority.LOW;
        [Required] public DeviceItem DeviceItem { get; set; }
        [Required] public IEnumerable<TagItem> TagItem { get; set; } = new List<TagItem>();
    }
}
