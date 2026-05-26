using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Repositories.Interfaces;

namespace P7CreateRestApi.Repositories
{
    public class RuleNameRepository : IRuleNameRepository
    {
        public Task<Rating> CreateAsync(RuleName ruleName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RuleName>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RuleName?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Rating> UpdateAsync(RuleName ruleName)
        {
            throw new NotImplementedException();
        }
    }
}
