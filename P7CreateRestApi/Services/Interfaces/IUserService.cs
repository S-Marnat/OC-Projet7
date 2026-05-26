using P7CreateRestApi.Models;

namespace P7CreateRestApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserReadDTO?> GetByIdAsync(int id);
        Task<IEnumerable<UserReadDTO>> GetAllAsync();
        Task<UserReadDTO> CreateAsync(UserCreateDTO dto);
        Task<UserReadDTO?> UpdateAsync(int id, UserUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
