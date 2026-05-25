using P7CreateRestApi.Models;

namespace P7CreateRestApi.Services.Interfaces
{
    public interface IRatingService
    {
        Task<RatingReadDTO?> GetByIdAsync(int id);
        Task<IEnumerable<RatingReadDTO>> GetAllAsync();
        Task<RatingReadDTO> CreateAsync(RatingCreateDTO dto);
        Task<RatingReadDTO?> UpdateAsync(int id, RatingUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
