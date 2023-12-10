using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PoolingWorker.Enum;

namespace PoolingWorker.Models.Domain
{
    public class RequestItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public EnumPoolingPriority Priority { get; set; } = EnumPoolingPriority.LOW;

        public DeviceItem DeviceItem { get; set; }
        public TagItem TagItem { get; set; }
    }
}
