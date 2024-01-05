using Microsoft.AspNetCore.SignalR;

namespace PoolingWorker.Hubs;

public class PoolingHub : Hub
{
    private readonly ILogger<PoolingHub> _logger;
    public PoolingHub(ILogger<PoolingHub>looger)
    {
        _logger = looger;
    }
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined");
    }

    public async Task SendMessage(string message)
    {
        _logger.LogInformation(message);
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}
