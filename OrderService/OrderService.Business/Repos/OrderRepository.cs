using Microsoft.EntityFrameworkCore;
using OrderService.Application;
using OrderService.Application.DbModels;
using OrderService.Application.Enums;

namespace OrderService.Business.Repos
{
    public class OrderRepository(OrderServiceContext context) : IOrderRepository
    {
        public async Task<DbOrder> CreateAsync(DbOrder order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();

            return order;
        }
        
        public async Task<int> UpdateAsync(Guid orderId, Status status, Guid productId)
        {
            DbOrder order = await context.Orders.FirstAsync(o => o.Id == orderId);

            order.Status = (int)status;
            order.ProductId = productId;
            
            return await context.SaveChangesAsync();
        }

        public async Task<string> GetEmailAsync(Guid orderId)
        {
            return await context.Orders.Where(o => o.Id == orderId).Select(o => o.Email).FirstAsync();
        }
    }
}