namespace OrderService.Business
{
    public interface IPublisher
    {
        Task SendOrderInfoAsync(Guid orderId, Guid productId, int count);
        Task SendNotificationAsync(Guid orderId, string message, string email);
    }
}