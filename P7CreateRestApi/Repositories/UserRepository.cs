using P7CreateRestApi.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Repositories.Interfaces;

namespace P7CreateRestApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User?> FindByUserName(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<User?> UpdateAsync(int id, string fullname)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
                return null;

            user.Fullname = fullname;

            await _userManager.UpdateAsync(user);

            return user;
        }
    }
}