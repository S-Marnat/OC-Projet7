using Dot.Net.WebApi.Domain;

namespace P7CreateRestApi.Repositories.Interfaces
{
    public interface IRuleNameRepository
    {
        Task<RuleName?> GetByIdAsync(int id);
        Task<IEnumerable<RuleName>> GetAllAsync();
        Task<RuleName> CreateAsync(RuleName ruleName);
        Task<RuleName> UpdateAsync(RuleName ruleName);
        Task<bool> DeleteAsync(int id);
    }
}
