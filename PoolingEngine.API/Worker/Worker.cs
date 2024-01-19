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
                    if (tagValues != null)
                    {
                        await _poolingBroadcastContext.Clients.All.SendAsync("PoolingResult", tagValues);
                    }
                });
            }
        }

        //private async Task DoWork()
        //{
        //    var requestItem = _requestItemRepository.RequestItems()
        //       .OrderByDescending(x => x.Priority)
        //       .ThenByDescending(x => x.TimeStamp).FirstOrDefault();
        //    if (requestItem != null)
        //    {
        //        using (var scope = _serviceProvider.CreateScope())
        //        {
        //            var context = scope.ServiceProvider.GetService<IUnitOfWork>();
        //            var deviceItem = context.DeviceItem.GetById(requestItem.DeviceItemId);
                    
        //            var listTagItems = context.TagGroup.LinkedTagItems(requestItem.TagGroups.ToList());
        //            if (listTagItems.Count > 0 && deviceItem != null)
        //            {
        //                var listTagDef = context.TagDef.ListDeviceTagDef(deviceItem, listTagItems);
        //                var poolResult = _opcUaRepository.OpcRead(deviceItem, listTagDef);
        //                if (poolResult != null)
        //                {
        //                    List<TagValue> tagValues = new List<TagValue>();
        //                    int i = 0;
        //                    foreach (var value in poolResult)
        //                    {
        //                        TagValue tagValue = new TagValue()
        //                        {
        //                            RequestItemId = requestItem.Id.ToString(),
        //                            DeviceItemId = requestItem.DeviceItemId,
        //                            TagItemId = listTagItems[i].Id,
        //                            Value = value.Value.ToString(),
        //                            TimeStamp = (DateTime)value.SourceTimestamp

        //                        };
        //                        tagValues.Add(tagValue);
        //                        i++;
        //                    }
        //                    await _poolingBroadcastContext.Clients.All.SendAsync("PoolingResult", tagValues);
        //                }
        //            }
        //        }
        //        _requestItemRepository.Remove(requestItem);
        //    }
        //}
    }
}
