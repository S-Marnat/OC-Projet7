using P7CreateRestApi.Models;

namespace P7CreateRestApi.Services.Interfaces
{
    public interface IRuleNameService
    {
        Task<RuleNameReadDTO?> GetByIdAsync(int id);
        Task<IEnumerable<RuleNameReadDTO>> GetAllAsync();
        Task<RuleNameReadDTO> CreateAsync(RuleNameCreateDTO dto);
        Task<RuleNameReadDTO?> UpdateAsync(int id, RuleNameUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
