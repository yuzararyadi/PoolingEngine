
using PoolingWorker.Enum;

namespace PoolingWorker.Models.Dtos
{
    public class RequestItemDto
    {
        public Guid Id { get; set; }
        public int DeviceId { get; set; }
        public int TagID { get; set; }
        public EnumPoolingPriority Priority { get; set; } = EnumPoolingPriority.LOW;

    }
}
