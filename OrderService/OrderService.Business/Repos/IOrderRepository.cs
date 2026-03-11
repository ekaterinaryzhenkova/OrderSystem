using OrderService.Application.DbModels;
using OrderService.Application.Enums;

namespace OrderService.Business.Repos
{
    public interface IOrderRepository
    {
        Task<DbOrder> CreateAsync(DbOrder order);
        
        Task<int> UpdateAsync(Guid orderId, Status status, Guid productId);
        
        Task<string> GetEmailAsync(Guid orderId);
    }
}