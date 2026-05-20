using P7CreateRestApi.Models;

namespace P7CreateRestApi.Services.Interfaces
{
    public interface IBidListService
    {
        Task<BidListReadDTO?> GetByIdAsync(int id);
        Task<IEnumerable<BidListReadDTO>> GetAllAsync();
        Task<BidListReadDTO> CreateAsync(BidListCreateDTO dto);
        Task<BidListReadDTO> UpdateAsync(int id, BidListUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
