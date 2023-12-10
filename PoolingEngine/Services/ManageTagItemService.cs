using Grpc.Core;
using PoolingEngine.Data;
using PoolingEngine.Models;
using PoolingEngine.Protos;

namespace PoolingEngine.Services
{
    public class ManageTagItemService : ManageTagItem.ManageTagItemBase
    {
        private readonly AppDbContext _dbContext;
        public ManageTagItemService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //private readonly ILogger<ManageTagItemService> _logger;
        //public ManageTagItemService(ILogger<ManageTagItemService> logger)
        //{
        //    _logger = logger;
        //}

        public override async Task<CreateTagItemResponse> CreateTag(CreateTagItemRequest request, ServerCallContext context)
        {
            TagItem tagItem = new TagItem
            {
                Name = request.TagName,
                Description = request.Description,
                DataType = (EnumDataType)System.Enum.Parse(typeof(EnumDataType), request.DataType)
            };
            await _dbContext.AddAsync(tagItem);
            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(new CreateTagItemResponse
            {
                Id = tagItem.Id,
                TagName = request.TagName,
            });

        }
    }
}

