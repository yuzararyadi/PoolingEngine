using System.ComponentModel.DataAnnotations;

namespace PoolingEngine.API.Dtos
{
    public class TagValueDto
    {
        public int Id { get; set; }
        public string RequestItemId { get; set; } = string.Empty;
        public int DeviceId { get; set; }
        public int TagId { get; set; }
        public string? Value { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}
