using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Repositories.Interfaces;

namespace P7CreateRestApi.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        public Task<Rating> CreateAsync(Rating rating)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Rating>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Rating?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Rating> UpdateAsync(Rating rating)
        {
            throw new NotImplementedException();
        }
    }
}
