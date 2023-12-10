using Microsoft.AspNetCore.SignalR;

namespace PoolingWorker.Hubs;

public class PoolingHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined");
    }

    public async Task SendMessage(int message) 
    { 
        await Clients.All.SendAsync("ReceiveMessage",message);
    }
}
