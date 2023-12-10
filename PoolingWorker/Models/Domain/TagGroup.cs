using System.ComponentModel.DataAnnotations;

namespace PoolingWorker.Models.Domain
{
    public class TagGroup
    {
        public TagGroup()
        {
            this.TagItems = new List<TagItem>();
            this.deviceItems = new List<DeviceItem>();
        }
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public ICollection<TagItem>? TagItems { get; set; }
        public ICollection<DeviceItem>? deviceItems { get; set; }
    }
}
