using MassTransit;
using OrderService.Business;
using SharedModels;

namespace OrderService.Broker
{
    public class Publisher(IPublishEndpoint publishEndpoint) : IPublisher
    {
        public Task SendOrderInfoAsync(Guid orderId, string productName, int count)
        {
            var request = new OrderCreated
            {
                OrderId = orderId,
                ProductName = productName,
                Count = count
            };

            return publishEndpoint.Publish(request);
        }
        
        public Task SendNotificationAsync(Guid orderId, string message, string email)
        {
            var request = new OrderNotification
            {
                OrderId = orderId,
                Message = message,
                Email = email
            };

            return publishEndpoint.Publish(request);
        }
    }
}