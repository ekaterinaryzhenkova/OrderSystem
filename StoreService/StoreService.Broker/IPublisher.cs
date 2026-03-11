namespace StoreService.Broker
{
    public interface IPublisher
    {
        Task SendStoreResponseAsync(Guid orderId, Guid ptoductId, bool isSuccess, string message);
    }
}