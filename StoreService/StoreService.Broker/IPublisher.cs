namespace StoreService.Broker
{
    public interface IPublisher
    {
        Task SendStoreResponseAsync(Guid orderId, bool isSuccess, string message);
    }
}