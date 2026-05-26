using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Repositories.Interfaces;

namespace P7CreateRestApi.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        public Task<Trade> CreateAsync(Trade trade)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Trade>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Trade?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Trade> UpdateAsync(Trade trade)
        {
            throw new NotImplementedException();
        }
    }
}
