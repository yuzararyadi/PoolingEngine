
using PoolingWorker.Enum;

namespace PoolingWorker.Models.Dtos
{
    public class TagItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public EnumDataType DataType { get; set; }
    }

}
