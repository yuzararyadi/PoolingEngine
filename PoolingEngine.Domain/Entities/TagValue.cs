using System.ComponentModel.DataAnnotations;

namespace PoolingEngine.Domain.Entities
{
    public class TagValue
    {
        public int Id { get; set; }
        [Required]
        public string RequestItemId { get; set; } = string.Empty;
        public int DeviceItemId { get; set; }
        public int TagItemId { get; set; }
        [MaxLength(20)]
        public string? Value { get; set; }
        public DateTime TimeStamp { get; set; }


        DeviceItem? DeviceItem { get; set; }
        TagItem? TagItem { get; set; }
    }
}
