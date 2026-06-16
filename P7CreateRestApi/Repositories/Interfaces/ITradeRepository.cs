using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories.Interfaces
{
    public interface ITradeRepository
    {
        Task<Trade?> GetByIdAsync(int id);
        Task<IEnumerable<Trade>> GetAllAsync();
        Task<Trade> CreateAsync(Trade trade);
        Task<Trade> UpdateAsync(Trade trade);
        Task<bool> DeleteAsync(int id);
    }
}
