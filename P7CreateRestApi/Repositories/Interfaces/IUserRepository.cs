using Dot.Net.WebApi.Domain;

namespace P7CreateRestApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> FindByUserName(string userName);
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> UpdateAsync(int id, string fullname);
    }
}
