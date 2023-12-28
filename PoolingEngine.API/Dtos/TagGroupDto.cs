using System.ComponentModel.DataAnnotations;

namespace PoolingEngine.API.Dtos
{
    public class TagGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<TagItemDto> TagItems = new HashSet<TagItemDto>();
    }
}
