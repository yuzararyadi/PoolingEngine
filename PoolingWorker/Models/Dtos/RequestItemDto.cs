using PoolingWorker.Enum;
using PoolingWorker.Models.Domain;

namespace PoolingWorker.Models.Dtos
{
    public class RequestItemDto
    {
        public Guid Id { get; set; }
        public int DeviceItemId { get; set; }
        public EnumPoolingPriority Priority { get; set; } = EnumPoolingPriority.LOW;
    }
}
