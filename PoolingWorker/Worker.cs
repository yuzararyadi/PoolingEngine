using Microsoft.AspNetCore.SignalR;
using PoolingWorker.Hubs;
using Opc.UaFx.Client;
using Newtonsoft.Json.Linq;
using Opc.UaFx;

namespace PoolingWorker
{
    public class Worker : BackgroundService
    {

        private readonly ILogger<Worker> _logger;
        private readonly IHubContext<PoolingHub> _poolingHub;

        public Worker(ILogger<Worker> logger, IHubContext<PoolingHub>poolingHub )
        {
            _logger = logger;
            _poolingHub = poolingHub;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var client = new OpcClient("opc.tcp://127.0.0.1:49320");
            client.Security.UserIdentity = new OpcClientIdentity("yuzar","yuzararyadiadmin");
            
            
            client.Connect();
            
            while(!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                if (client.State == OpcClientState.Connected || client.State == OpcClientState.Reconnected)
                {

                    List<OpcNodeId> NodeIds = new List<OpcNodeId>();

                    for (var i = 22; i < 30; i++)
                        NodeIds.Add($"ns=2;s=Weatherford.Wellpilot1.Parameters 0000-0300.Param_000{i}_Value_B");



                    //var value = client.ReadNodeValues("ns=2;s=Weatherford.Wellpilot1.Parameters 0000-0300.Param_00021_Value_B", "ns=2;s=Weatherford.Wellpilot1.Parameters 0000-0300.Param_00022_Value_B");
                    //var value1 = client.ReadNode("ns=2;s=Weatherford.Wellpilot1.Parameters 0000-0300.Param_00022_Value_B");
                    //var value2 = client.ReadNode("ns=2;s=Weatherford.Wellpilot1.Parameters 0000-0300.Param_00023_Value_B");
                    //var value3 = client.ReadNode("ns=2;s=Weatherford.Wellpilot1.Parameters 0000-0300.Param_00024_Value_B");
                    //var value4 = client.ReadNode("ns=2;s=Weatherford.Wellpilot1.Parameters 0000-0300.Param_00025_Value_B");
                    var value = client.ReadNodes(NodeIds);
                    await _poolingHub.Clients.All.SendAsync("ReceiveMessage", value, stoppingToken);
                }
                else
                {
                    await _poolingHub.Clients.All.SendAsync("ReceiveMessage", client.State.ToString(), stoppingToken);
                }

                await Task.Delay(1000, stoppingToken);
            }

            client.Disconnect();

        }
        
    }
}
