using StoreService.Data.DbModels;

namespace StoreService.Business.Repos
{
    public interface IStoreRepository
    {
        Task<DbProduct?> GetAsync(string productName);
        
        Task<int> UpdateAsync(DbProduct product, int quantity);
    }
}