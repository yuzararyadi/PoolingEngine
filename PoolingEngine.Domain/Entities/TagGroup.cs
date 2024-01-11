using System.ComponentModel.DataAnnotations;

namespace PoolingEngine.Domain.Entities
{
    public class TagGroup
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;
        public ICollection<TagItem>? TagItems { get; set; } = new HashSet<TagItem>();
        public ICollection<DeviceItem>? deviceItems { get; set; } = new HashSet<DeviceItem>();
        public ICollection<RequestItem> RequestItems { get; set; } = new HashSet<RequestItem>();
    }
}
