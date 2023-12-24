using PoolingEngine.Domain.Enum;

namespace PoolingEngine.API.Dtos
{
    public class RequestItemDto
    {
        public Guid Id { get; set; }
        public int DeviceItemId { get; set; }
        public EnumPoolingPriority Priority { get; set; } = EnumPoolingPriority.LOW;
    }
}
