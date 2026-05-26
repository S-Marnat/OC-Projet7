using P7CreateRestApi.Models;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Services
{
    public class TradeService : ITradeService
    {
        public Task<TradeReadDTO> CreateAsync(TradeCreateDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TradeReadDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TradeReadDTO?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TradeReadDTO?> UpdateAsync(int id, TradeUpdateDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
