using OrderService.Application.DbModels;
using OrderService.Application.Enums;
using OrderService.Application.Models;
using OrderService.Business.Repos;

namespace OrderService.Business.Commands
{
    public class CreateOrder(
        IOrderRepository repository,
        IPublisher publisher)
        : ICreateOrder
    {
        public async Task<bool> ExecuteAsync(OrderRequest request)
        {
            var order = new DbOrder()
            {
                Id = Guid.NewGuid(),
                ProductName = request.ProductName,
                Count = request.Count,
                Email = request.Email,
                Status = (int)Status.Created
            };

            await repository.CreateAsync(order);
            await publisher.SendOrderInfoAsync(order.Id, order.ProductName, order.Count);

            return true;
        }
    }
}