namespace OrderService.Business
{
    public interface IPublisher
    {
        Task SendOrderInfoAsync(Guid orderId, string productName, int count);
        
        Task SendNotificationAsync(Guid orderId, string message, string email);
    }
}