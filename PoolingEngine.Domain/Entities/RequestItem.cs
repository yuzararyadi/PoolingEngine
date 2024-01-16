using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PoolingEngine.Domain.Enum;

namespace PoolingEngine.Domain.Entities
{
    public class RequestItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string RequestPoolingId { get; set; } = string.Empty;
        public EnumPoolingPriority Priority { get; set; } = EnumPoolingPriority.LOW;
        public int DeviceItemId { get; set; }
        //public IEnumerable<int> TagGroupsIds { get; set; } = new List<int>();
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public IEnumerable<TagGroup> TagGroups { get; set; } = new HashSet<TagGroup>();
    }
}
