using MassTransit;
using SharedModels;
using StoreService.Business.Repos;
using StoreService.Data.DbModels;

namespace StoreService.Broker.Consumers
{
    public class CreateOrderConsumer(
        IPublisher publisher,
        IStoreRepository repository)
        : IConsumer<OrderCreated>
    {
        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            var orderInfo = context.Message;

            DbProduct? product = await repository.GetAsync(orderInfo.ProductName);

            if (product is null)
            {
                await publisher.SendStoreResponseAsync(orderInfo.OrderId, Guid.Empty, false, "К сожалению, данный товара нет в наличии.");
                return;
            }

            if (product.Quantity - orderInfo.Count < 0)
            {
                await publisher.SendStoreResponseAsync(orderInfo.OrderId, product.Id, false, "К сожалению, данный товар закончился.");
                return;
            }

            await repository.UpdateAsync(product, product.Quantity - orderInfo.Count);
            await publisher.SendStoreResponseAsync(orderInfo.OrderId, product.Id, true, "Заказ подтвержден.");
        }
    }
}