using System.ComponentModel.DataAnnotations;

namespace PoolingEngine.Models
{
    public class TagValue
    {
        [Key]
 //       Guid Id { get; set; }
        public int Id { get; set; }
        [Required]
        public string RequestItemId { get; set; }
        DeviceItem? DeviceItem { get; set; }
        TagItem? TagItem { get; set; }
        public string? Value { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
