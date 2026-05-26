using P7CreateRestApi.Models;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Services
{
    public class RuleNameService : IRuleNameService
    {
        public Task<RuleNameReadDTO> CreateAsync(RuleNameCreateDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RuleNameReadDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RuleNameReadDTO?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<RuleNameReadDTO?> UpdateAsync(int id, RuleNameUpdateDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
