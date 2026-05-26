using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models;
using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserReadDTO> CreateAsync(UserCreateDTO dto)
        {
            // Mapper DTO -> Domain
            var entite = new User
            {
                UserName = dto.UserName,
                Password = dto.Password,
                Fullname = dto.Fullname,
                Role = dto.Role
            };

            // Appeler le repository
            var creer = await _repository.CreateAsync(entite);

            // Mapper Domain -> DTO Read
            return new UserReadDTO
            {
                Id = creer.Id,
                UserName = creer.UserName,
                Fullname = creer.Fullname,
                Role = creer.Role
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Appeler le repository
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UserReadDTO>> GetAllAsync()
        {
            // Appeler le repository
            var entites = await _repository.GetAllAsync();

            // Mapper Domain -> DTO Read
            return entites.Select(e => new UserReadDTO
            {
                Id = e.Id,
                UserName = e.UserName,
                Fullname = e.Fullname,
                Role = e.Role
            });
        }

        public async Task<UserReadDTO?> GetByIdAsync(int id)
        {
            // Appeeler le repository
            var entite = await _repository.GetByIdAsync(id);

            if (entite == null)
                return null;

            // Mapper Domain -> DTO Read
            return new UserReadDTO
            {
                Id = entite.Id,
                UserName = entite.UserName,
                Fullname = entite.Fullname,
                Role = entite.Role
            };
        }

        public async Task<UserReadDTO?> UpdateAsync(int id, UserUpdateDTO dto)
        {
            // Récupérer l'entité existante
            var entite = await _repository.GetByIdAsync(id);

            if (entite == null)
                return null;

            // Mapper DTO -> Domain
            entite.UserName = dto.UserName;
            entite.Password = dto.Password;
            entite.Fullname = dto.Fullname;
            entite.Role = dto.Role;

            // Appeler le repository
            var mettreAJour = await _repository.UpdateAsync(entite);

            // Mapper Domain -> DTO Read
            return new UserReadDTO
            {
                Id = mettreAJour.Id,
                UserName = mettreAJour.UserName,
                Fullname = mettreAJour.Fullname,
                Role = mettreAJour.Role,
            };
        }
    }
}
