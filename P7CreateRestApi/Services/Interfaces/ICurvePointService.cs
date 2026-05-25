using P7CreateRestApi.Models;

namespace P7CreateRestApi.Services.Interfaces
{
    public interface ICurvePointService
    {
        Task<CurvePointReadDTO?> GetByIdAsync(int id);
        Task<IEnumerable<CurvePointReadDTO>> GetAllAsync();
        Task<CurvePointReadDTO> CreateAsync(CurvePointCreateDTO dto);
        Task<CurvePointReadDTO?> UpdateAsync(int id, CurvePointUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
