using P7CreateRestApi.Models;

namespace P7CreateRestApi.Services.Interfaces
{
    public interface ITradeService
    {
        Task<TradeReadDTO?> GetByIdAsync(int id);
        Task<IEnumerable<TradeReadDTO>> GetAllAsync();
        Task<TradeReadDTO> CreateAsync(TradeCreateDTO dto);
        Task<TradeReadDTO?> UpdateAsync(int id, TradeUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
