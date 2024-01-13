using Microsoft.AspNetCore.SignalR;

namespace PoolingEngine.API.Hub
{
    public class PoolingBroadcastHub : Hub<IPoolingBroadcastHub>
    {
        public async Task Send(string message)
        {
            await Clients.All.SendPoolingResult(message);
        }
    }
}
