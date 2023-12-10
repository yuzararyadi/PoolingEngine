using System.ComponentModel.DataAnnotations;

namespace PoolingWorker.Models.Domain
{
    public class DeviceItem
    {
        public DeviceItem()
        {
            this.TagGroups = new HashSet<TagGroup>();
            this.TagItems = new HashSet<TagItem>();
        }
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string? Description { get; set; }

        public ICollection<TagGroup>? TagGroups { get; set; }
        public ICollection<TagItem>? TagItems { get; set; }
    }
}
