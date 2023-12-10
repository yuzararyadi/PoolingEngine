using Grpc.Core;
using PoolingEngine.Data;
using PoolingEngine.Models;
using PoolingEngine.Protos;

namespace PoolingEngine.Services
{
    public class ManageDeviceItemService : ManageDeviceItem.ManageDeviceItemBase
    {
        private readonly AppDbContext _dbContext;
        public ManageDeviceItemService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //private readonly ILogger<ManageDeviceItemService> _logger;
        //public ManageDeviceItemService(ILogger<ManageDeviceItemService> logger)
        //{
        //    _logger = logger;
        //}

        public override async Task<CreateDeviceItemResponse> CreateDevice(CreateDeviceItemRequest request, ServerCallContext context)
        {
            DeviceItem DeviceItem = new DeviceItem
            {
                Name = request.DeviceName,
                Description = request.Description
                
            };
            await _dbContext.AddAsync(DeviceItem);
            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(new CreateDeviceItemResponse
            {
                Id = DeviceItem.Id,
                DeviceName = request.DeviceName,
            });

        }
    }
}

