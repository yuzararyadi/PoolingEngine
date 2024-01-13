using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PoolingEngine.API.Hub;
using PoolingEngine.DataAccess.Context;
using PoolingEngine.Domain.Entities;
using PoolingEngine.Domain.Repository.WorkerRepository;

namespace PoolingEngine.API.Worker
{
    public class PoolingExecution : IPoolingExecution
    {
        private readonly IHubContext<PoolingBroadcastHub> _poolingBroadcastContext;
        InMemoryDbContext _inMemoryDbContext;
        public PoolingExecution(InMemoryDbContext inMemoryDbContext,IHubContext<PoolingBroadcastHub> hubContext)
        {
            _inMemoryDbContext = inMemoryDbContext;
            _poolingBroadcastContext = hubContext;
        }
        public async Task DoRequestItemPooling(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var requestItem = _inMemoryDbContext.RequestItems.ToList()
                    .OrderByDescending(x => x.Priority)
                    .ThenByDescending(x => x.TimeStamp).FirstOrDefault();
                if (requestItem != null)
                {
                    await _poolingBroadcastContext.Clients.All.SendAsync("PoolingResult", requestItem);
                    _inMemoryDbContext.RequestItems.Remove(requestItem);
                    //_inMemoryDbContext.SaveChanges();
                    await Task.Delay(1000, stoppingToken);
                }
                else
                {
                    //Task.CompletedTask();
                }

            }
        }

    }
}
