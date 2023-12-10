using System.ComponentModel.DataAnnotations;

namespace PoolingWorker.Models
{
    public class DeviceTagGroups
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public DeviceItem Devices { get; set; }
        public List<TagGroup> TagGroups { get; set; }
    }
}
