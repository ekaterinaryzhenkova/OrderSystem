using StoreService.Data.DbModels;

namespace StoreService.Business.Repos
{
    public interface IStoreRepository
    {
        Task<DbProduct?> GetAsync(Guid productId);
        Task<int> UpdateAsync(DbProduct product, int quantity);
    }
}