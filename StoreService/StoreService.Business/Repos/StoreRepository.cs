using Microsoft.EntityFrameworkCore;
using StoreService.Data;
using StoreService.Data.DbModels;

namespace StoreService.Business.Repos
{
    public class StoreRepository(StoreServiceContext context) : IStoreRepository
    {
        public async Task<DbProduct?> GetAsync(string productName)
        {
            return await context.Products
                .FirstOrDefaultAsync(p => p.Name.ToLower() == productName.ToLower());
        }

        public async Task<int> UpdateAsync(DbProduct product, int quantity)
        {
            product.Quantity = quantity;
            return await context.SaveChangesAsync();
        }
    }
}