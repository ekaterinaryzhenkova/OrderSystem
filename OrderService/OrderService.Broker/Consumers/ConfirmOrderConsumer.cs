using MassTransit;
using OrderService.Application.Enums;
using OrderService.Business;
using OrderService.Business.Repos;
using StoreResponse = SharedModels.StoreResponse;

namespace OrderService.Broker.Consumers
{
    public class ConfirmOrderConsumer(
        IOrderRepository repository,
        IPublisher publisher)
        : IConsumer<StoreResponse>
    {
        public async Task Consume(ConsumeContext<StoreResponse> context)
        {
            var response = context.Message;
            
            var orderEmail =  await repository.GetEmailAsync(response.OrderId);
            var status = response.IsSuccess
                ? Status.Confirmed
                : Status.Rejected;

            await repository.UpdateAsync(response.OrderId, status);
            await publisher.SendNotificationAsync(response.OrderId, response.Message, orderEmail);
        }
    }
}