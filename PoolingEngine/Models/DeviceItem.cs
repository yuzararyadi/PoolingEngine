using System.ComponentModel.DataAnnotations;

namespace PoolingEngine.Models
{
    public class DeviceItem
    {
        [Key]
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
