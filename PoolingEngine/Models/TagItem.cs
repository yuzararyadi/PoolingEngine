using System.ComponentModel.DataAnnotations;

namespace PoolingEngine.Models
{
    public class TagItem
    {
        [Key]
        //        public Guid Id { get; } = Guid.NewGuid
        public int Id { get;set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public EnumDataType DataType { get; set; }
    }

}
