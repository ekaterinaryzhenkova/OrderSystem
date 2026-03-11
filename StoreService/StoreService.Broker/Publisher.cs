using MassTransit;
using SharedModels;

namespace StoreService.Broker
{
    public class Publisher(IPublishEndpoint publishEndpoint) : IPublisher
    {
        public Task SendStoreResponseAsync(Guid orderId, Guid ptoductId, bool isSuccess, string message)
        {
            var request = new StoreResponse
            {
                OrderId = orderId,
                ProductId = ptoductId,
                IsSuccess = isSuccess,
                Message = message
            };

            return publishEndpoint.Publish(request);
        }
    }
}