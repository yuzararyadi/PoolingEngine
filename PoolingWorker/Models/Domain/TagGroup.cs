using System.ComponentModel.DataAnnotations;

namespace PoolingWorker.Models.Domain
{
    public class TagGroup
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public ICollection<TagItem>? TagItems { get; set; } = new HashSet<TagItem>();
        public ICollection<DeviceItem>? deviceItems { get; set; } = new HashSet<DeviceItem>();
    }
}
