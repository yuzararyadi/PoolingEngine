using System.ComponentModel.DataAnnotations;

namespace PoolingEngine.Domain.Entities
{
    public class TagValue
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string RequestItemId { get; set; } = string.Empty;
        public int DeviceId { get; set; }
        public int TagId { get; set; }
        [MaxLength(20)]
        public string? Value { get; set; }
        public DateTime TimeStamp { get; set; }


        DeviceItem? DeviceItem { get; set; }
        TagItem? TagItem { get; set; }
    }
}
