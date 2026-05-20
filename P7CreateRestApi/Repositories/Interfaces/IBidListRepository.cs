using Dot.Net.WebApi.Domain;

namespace P7CreateRestApi.Repositories.Interfaces
{
    public interface IBidListRepository
    {
        Task<BidList?> GetByIdAsync(int id);
        Task<IEnumerable<BidList>> GetAllAsync();
        Task<BidList> CreateAsync(BidList bidList);
        Task<BidList> UpdateAsync(BidList bidList);
        Task<bool> DeleteAsync(int id);
    }
}
