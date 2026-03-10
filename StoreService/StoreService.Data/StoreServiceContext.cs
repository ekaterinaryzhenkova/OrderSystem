using Microsoft.EntityFrameworkCore;
using StoreService.Data.DbModels;

namespace StoreService.Data
{
    public class StoreServiceContext(
        DbContextOptions<StoreServiceContext> options)
        : DbContext(options)
    {
        public DbSet<DbProduct> Products { get; init; }
    }
}