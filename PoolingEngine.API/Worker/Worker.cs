using PoolingEngine.Domain.Repository;
using Microsoft.AspNetCore.SignalR;
using PoolingEngine.API.Hub;
using PoolingEngine.Domain.Entities;

namespace PoolingEngine.API.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IRequestItemRepository _requestItemRepository;
        private readonly IHubContext<PoolingBroadcastHub> _poolingBroadcastContext;

        public Worker(IHubContext<PoolingBroadcastHub> hubContext, 
            IRequestItemRepository requestItemRepository)
        {
            _poolingBroadcastContext = hubContext;
            _requestItemRepository = requestItemRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Run(async () =>
                {
                    List<TagValue> tagValues = new List<TagValue>();
                    tagValues = _requestItemRepository.ExecuteRequestItem();
                    if (tagValues.Count > 0)
                    {
                        await _poolingBroadcastContext.Clients.All.SendAsync("PoolingResult", tagValues);
                    }
                });
            }
        }
    }
}
