using System.ComponentModel.DataAnnotations;
using PoolingWorker.Enum;

namespace PoolingWorker.Models.Domain
{
    public class TagItem
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Description { get; set; }
        public EnumDataType DataType { get; set; }

        public ICollection<DeviceItem>? DeviceItems { get; set; } = new HashSet<DeviceItem>();
        public ICollection<TagGroup>? TagGroups { get; set; } = new HashSet<TagGroup>();
    }

}
