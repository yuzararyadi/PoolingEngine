using PoolingEngine.Domain.Enum;

namespace PoolingEngine.API.Dtos
{
    public class RequestItemDto
    {
        public Guid Id { get; set; }
        public string RequestPoolingId { get; set; } = string.Empty;
        public int? DeviceItemId { get; set; }
        public EnumPoolingPriority Priority { get; set; } = EnumPoolingPriority.LOW;
        public DateTime TimeStamp { get; set; }
        public IEnumerable<TagGroupDto> TagGroups { get; set; } = new HashSet<TagGroupDto>();
    }
}
