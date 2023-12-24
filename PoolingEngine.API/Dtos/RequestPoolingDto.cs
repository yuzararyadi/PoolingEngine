using PoolingEngine.Domain.Enum;

namespace PoolingEngine.API.Dtos
{

    public class RequestPoolingDto
    {
        public IEnumerable<int> DeviceItemIds { get; set; } = new HashSet<int>();
        public IEnumerable<int>? TagGroupIds { get; set; }
        public IEnumerable<int>? TagIDs { get; set; }
        public EnumPoolingPriority Priority { get; set; } = EnumPoolingPriority.LOW;

    }
}
