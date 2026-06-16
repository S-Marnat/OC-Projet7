using P7CreateRestApi.Domain;
using Microsoft.AspNetCore.Identity;
using P7CreateRestApi.Models;
using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly UserManager<User> _userManager;

        public UserService(IUserRepository repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserReadDTO>> GetAllAsync()
        {
            // Appeler le repository
            var entites = await _repository.GetAllAsync();
            var resultat = new List<UserReadDTO>();

            // Mapper Domain -> DTO Read
            foreach (var entite in entites)
            {
                var roles = await _userManager.GetRolesAsync(entite);

                resultat.Add(new UserReadDTO
                {
                    Id = entite.Id,
                    UserName = entite.UserName,
                    Fullname = entite.Fullname,
                    Role = roles.FirstOrDefault()
                });
            }

            return resultat;
        }

        public async Task<UserReadDTO?> GetByIdAsync(int id)
        {
            // Appeler le repository
            var entite = await _repository.GetByIdAsync(id);

            if (entite == null)
                return null;

            var roles = await _userManager.GetRolesAsync(entite);

            // Mapper Domain -> DTO Read
            return new UserReadDTO
            {
                Id = entite.Id,
                UserName = entite.UserName,
                Fullname = entite.Fullname,
                Role = roles.FirstOrDefault()
            };
        }

        public async Task<UserReadDTO?> UpdateAsync(int id, UserUpdateDTO dto)
        {
            // Récupérer l'entité existante
            var entite = await _repository.GetByIdAsync(id);

            if (entite == null)
                return null;

            // Mapper DTO -> Domain
            entite.Fullname = dto.Fullname;

            // Appeler le repository
            var mettreAJour = await _repository.UpdateAsync(id, entite.Fullname);

            if (mettreAJour == null)
                return null;

            var roles = await _userManager.GetRolesAsync(mettreAJour);

            // Mapper Domain -> DTO Read
            return new UserReadDTO
            {
                Id = mettreAJour.Id,
                UserName = mettreAJour.UserName,
                Fullname = mettreAJour.Fullname,
                Role = roles.FirstOrDefault()
            };
        }
    }
}
