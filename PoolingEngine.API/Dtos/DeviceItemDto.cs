
namespace PoolingEngine.API.Dtos
{
    public class DeviceItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public IEnumerable<TagGroupDto> TagGroups { get; set; } = new HashSet<TagGroupDto>();

    }
}
