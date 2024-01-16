using PoolingEngine.DataAccess.Context;
using PoolingEngine.Domain.Repository;
using Microsoft.AspNetCore.SignalR;
using PoolingEngine.API.Hub;
using PoolingEngine.DataAccess.Implementation;
using PoolingEngine.Domain.Repository.WorkerRepository;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using PoolingEngine.Domain.Entities;

namespace PoolingEngine.API.Worker
{
    public class Worker : BackgroundService
    {
        //private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<PoolingBroadcastHub> _poolingBroadcastContext;
        private readonly IRequestItemRepository _requestItemRepository;
        private readonly IServiceProvider _serviceProvider;
        //private readonly IUnitOfWork _unitOfWork;
        private readonly IOpcUaRepository _opcUaRepository;
        public Worker(IHubContext<PoolingBroadcastHub> hubContext, 
            IRequestItemRepository requestItemRepository, 
            IServiceProvider serviceProvider, 
            IOpcUaRepository opcUaRepository)
        {
            _poolingBroadcastContext = hubContext;
            _requestItemRepository = requestItemRepository;
            _serviceProvider = serviceProvider;
            _opcUaRepository = opcUaRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                
                await Task.Run(async () =>
                {
                    var requestItem = _requestItemRepository.RequestItems()
                       .OrderByDescending(x => x.Priority)
                       .ThenByDescending(x => x.TimeStamp).FirstOrDefault();
                    if (requestItem != null)
                    {
                        
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var context = scope.ServiceProvider.GetService<IUnitOfWork>();
                            var listTagItems = context.TagGroup.LinkedTagItems(requestItem.TagGroups.ToList());
                            var deviceItem = context.DeviceItem.GetById(requestItem.DeviceItemId);
                            if (listTagItems.Count > 0 && deviceItem != null)
                            {
                                var listTagDef = context.TagDef.ListDeviceTagDef(deviceItem, listTagItems);
                                var poolResult = _opcUaRepository.OpcRead(deviceItem, listTagDef);
                                if (poolResult != null)
                                {
                                    int i = 0;
                                    foreach(var value in poolResult)
                                    {
                                        TagValue tagValue = new TagValue()
                                        {
                                            RequestItemId = requestItem.Id.ToString(),
                                            DeviceItemId = requestItem.DeviceItemId,
                                            TagItemId = listTagItems[i].Id,
                                            Value = value.Value.ToString(),
                                            TimeStamp = (DateTime)value.SourceTimestamp
                                            
                                        };
                                        await _poolingBroadcastContext.Clients.All.SendAsync("PoolingResult",tagValue);
                                        i++;
                                    }
                                    Console.WriteLine(poolResult);
                                }
                                    //await _poolingBroadcastContext.Clients.All.SendAsync((string)poolResult);
                            }
                        }
                        
                        _requestItemRepository.Remove(requestItem);
                    }
                });
                
            }
        }
    }
}
