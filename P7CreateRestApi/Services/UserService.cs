using P7CreateRestApi.Models;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Services
{
    public class UserService : IUserService
    {
        public Task<UserReadDTO> CreateAsync(UserCreateDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserReadDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserReadDTO?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserReadDTO?> UpdateAsync(int id, UserUpdateDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
