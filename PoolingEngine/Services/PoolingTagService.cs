
using PoolingEngine.Data;
using PoolingEngine.Models;
using PoolingEngine.Protos;
using Grpc.Core;

namespace PoolingEngine.Services
{
    public class PoolingTagService : PoolOpcTag.PoolOpcTagBase
    {
       
        private readonly AppDbContext _dbContext;
        private readonly ILogger<PoolingTagService> _logger;
        public PoolingTagService(AppDbContext dbContext, ILogger<PoolingTagService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public override async Task<poolOpcTagResponse> poolTag(poolOpcTagRequest request, ServerCallContext context)
        {
            DeviceItem? deviceItem = _dbContext.DeviceItems.Find(request.DeviceItemId);
            TagItem? tagItem = _dbContext.TagItems.Find(request.TagItemId);
            
            
            RequestItem requestItem = new RequestItem
            {
                Device = deviceItem,
                Tag = tagItem,
                Priority = (EnumPoolingPriority)System.Enum.Parse(typeof(EnumPoolingPriority),request.Priority),
            };
            await _dbContext.AddAsync(requestItem);
            string requestItemId = requestItem.Id.ToString();
            TagValue tagValue = new TagValue
            {
                RequestItemId = requestItemId,
                Value = "Test Value",
                TimeStamp = DateTime.UtcNow
            };
            
            await _dbContext.AddAsync(tagValue);
            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(new poolOpcTagResponse
            {
                RequestItemId = requestItemId,
                DeviceItemId = request.DeviceItemId,
                TagItemId = request.TagItemId,
                Value = tagValue.Value
            });

        }
    }
}
