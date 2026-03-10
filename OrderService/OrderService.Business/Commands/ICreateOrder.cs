using OrderService.Application.Models;

namespace OrderService.Business.Commands
{
    public interface ICreateOrder
    {
        Task<bool> ExecuteAsync(OrderRequest request);
    }
}