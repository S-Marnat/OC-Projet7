using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories.Interfaces
{
    public interface IRatingRepository
    {
        Task<Rating?> GetByIdAsync(int id);
        Task<IEnumerable<Rating>> GetAllAsync();
        Task<Rating> CreateAsync(Rating rating);
        Task<Rating> UpdateAsync(Rating rating);
        Task<bool> DeleteAsync(int id);
    }
}
