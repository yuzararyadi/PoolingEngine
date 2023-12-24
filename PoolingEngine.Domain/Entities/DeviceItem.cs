using System.ComponentModel.DataAnnotations;

namespace PoolingEngine.Domain.Entities
{
    public class DeviceItem
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? Description { get; set; }

        public ICollection<TagGroup>? TagGroups { get; set; } = new HashSet<TagGroup>();
        public ICollection<TagItem>? TagItems { get; set; } = new HashSet<TagItem>();
    }
}
