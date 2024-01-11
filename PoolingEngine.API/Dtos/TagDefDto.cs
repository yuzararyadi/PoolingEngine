namespace PoolingEngine.API.Dtos
{
    public class TagDefDto
    {
        public int Id { get; set; }
        public int DeviceItemId { get; set; }
        public int TagItemId { get; set; }
        public string MapAddress { get; set; } = string.Empty;
    }
}
