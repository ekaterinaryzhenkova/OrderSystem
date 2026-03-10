using Microsoft.EntityFrameworkCore;
using OrderService.Application.DbModels;

namespace OrderService.Application
{
    public class OrderServiceContext(
        DbContextOptions<OrderServiceContext> options)
        : DbContext(options)
    {
        public DbSet<DbOrder> Orders { get; init; }
    }
}