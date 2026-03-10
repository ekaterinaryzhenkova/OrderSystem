using MassTransit;
using SharedModels;

namespace StoreService.Broker
{
    public class Publisher(IPublishEndpoint publishEndpoint) : IPublisher
    {
        public Task SendStoreResponseAsync(Guid orderId, bool isSuccess, string message)
        {
            var request = new StoreResponse
            {
                OrderId = orderId,
                IsSuccess = isSuccess,
                Message = message
            };

            return publishEndpoint.Publish(request);
        }
    }
}