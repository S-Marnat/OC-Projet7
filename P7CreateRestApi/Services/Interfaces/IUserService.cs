using P7CreateRestApi.Models;

namespace P7CreateRestApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserReadDTO?> GetByIdAsync(int id);
        Task<IEnumerable<UserReadDTO>> GetAllAsync();
        Task<UserReadDTO?> UpdateAsync(int id, UserUpdateDTO dto);
    }
}
