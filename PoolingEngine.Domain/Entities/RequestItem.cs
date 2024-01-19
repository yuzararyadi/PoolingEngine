using PoolingEngine.Domain.Entities.Enum;
using PoolingEngine.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoolingEngine.Domain.Entities
{
    public class RequestItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string RequestPoolingId { get; set; } = string.Empty;
        public EnumPoolingPriority Priority { get; set; } = EnumPoolingPriority.LOW;
        public int DeviceItemId { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public EnumRequestType RequestType { get; set; } = EnumRequestType.READ;    //set read as default
        [NotMapped]
        public WriteItem? WriteItem { get; set; }
        public IEnumerable<TagGroup> TagGroups { get; set; } = new HashSet<TagGroup>();     //Taggroups to read
    }
}
