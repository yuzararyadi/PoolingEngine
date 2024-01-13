using PoolingEngine.DataAccess.Context;
using PoolingEngine.Domain.Repository;
using Microsoft.AspNetCore.SignalR;
using PoolingEngine.API.Hub;
using PoolingEngine.DataAccess.Implementation;
using PoolingEngine.Domain.Repository.WorkerRepository;

namespace PoolingEngine.API.Worker
{
    public class Worker : BackgroundService
    {
        //private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<PoolingBroadcastHub> _poolingBroadcastContext;
        public IServiceProvider _serviceProvider;
        public Worker(IServiceProvider serviceProvider, IHubContext<PoolingBroadcastHub> hubContext)
        {
            _serviceProvider = serviceProvider;
            _poolingBroadcastContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //await DoWork(stoppingToken);
                
                await Task.Run(() =>
                {
                    _ = DoWork(stoppingToken);
                });
            }
        }
        private async Task DoWork(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetRequiredService<IPoolingExecution>();
                await ctx.DoRequestItemPooling(stoppingToken);
            }
        }
    }
}
