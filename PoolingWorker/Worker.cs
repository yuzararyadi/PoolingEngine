using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using PoolingWorker.Hubs;
using Opc.UaFx.Client;
using Newtonsoft.Json.Linq;
using Opc.UaFx;

namespace PoolingWorker
{
    public class Worker : BackgroundService
    {
        HubConnection hubConnection;
        private readonly ILogger<Worker> _logger;
        private readonly IHubContext<PoolingHub> _poolingHub;

        public Worker(ILogger<Worker> logger, IHubContext<PoolingHub>poolingHub )
        {
            _logger = logger;
            _poolingHub = poolingHub;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var client = new OpcClient();

            //client.Security.UserIdentity = new OpcClientIdentity("yuzar","yuzararyadiadmin");
            

            while (!stoppingToken.IsCancellationRequested)
            {

                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //if (client.State == OpcClientState.Connected || client.State == OpcClientState.Reconnected)
                //{


                //    List<OpcNodeId> NodeIds = new List<OpcNodeId>();

                //    for (var i = 22; i < 30; i++)
                //        NodeIds.Add($"ns=2;s=Weatherford.Wellpilot1.Parameters 0000-0300.Param_000{i}_Value_B");

                //    var value = client.ReadNodes(NodeIds);
                //    await _poolingHub.Clients.All.SendAsync("ReceiveMessage", value, stoppingToken);
                //}
                //else
                //{
                //    await _poolingHub.Clients.All.SendAsync("ReceiveMessage", client.State.ToString(), stoppingToken);
                //}


                //await Task.Delay(1000, stoppingToken);
            }

            //client.Disconnect();

        }
        
    }
}
