namespace PoolingEngine.API.Hub
{
    public interface IPoolingBroadcastHub
    {
        Task SendPoolingResult(string message);
    }
}
