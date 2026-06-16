using P7CreateRestApi.Domain;
using P7CreateRestApi.Models;
using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Services
{
    public class RuleNameService : IRuleNameService
    {
        private readonly IRuleNameRepository _repository;

        public RuleNameService(IRuleNameRepository repository)
        {
            _repository = repository;
        }

        public async Task<RuleNameReadDTO> CreateAsync(RuleNameCreateDTO dto)
        {
            // Mapper DTO -> Domain
            var entite = new RuleName
            {
                Name = dto.Name,
                Description = dto.Description,
                Json = dto.Json,
                Template = dto.Template,
                SqlStr = dto.SqlStr,
                SqlPart = dto.SqlPart
            };

            // Appeler le repository
            var creer = await _repository.CreateAsync(entite);

            // Mapper Domain -> DTO Read
            return new RuleNameReadDTO
            {
                Id = creer.Id,
                Name = creer.Name,
                Description = creer.Description,
                Json = creer.Json,
                Template = creer.Template,
                SqlStr = creer.SqlStr,
                SqlPart = creer.SqlPart
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Appeler le repository
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<RuleNameReadDTO>> GetAllAsync()
        {
            // Appeler le repository
            var entites = await _repository.GetAllAsync();

            // Mapper Domain -> DTO Read
            return entites.Select(e => new RuleNameReadDTO
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Json = e.Json,
                Template = e.Template,
                SqlStr = e.SqlStr,
                SqlPart = e.SqlPart
            });
        }

        public async Task<RuleNameReadDTO?> GetByIdAsync(int id)
        {
            // Appeler le repository
            var entite = await _repository.GetByIdAsync(id);

            if (entite == null)
                return null;

            // Mapper Domain -> DTO Read
            return new RuleNameReadDTO
            {
                Id = entite.Id,
                Name = entite.Name,
                Description = entite.Description,
                Json = entite.Json,
                Template = entite.Template,
                SqlStr = entite.SqlStr,
                SqlPart = entite.SqlPart
            };
        }

        public async Task<RuleNameReadDTO?> UpdateAsync(int id, RuleNameUpdateDTO dto)
        {
            // Récupérer l'entité existante
            var entite = await _repository.GetByIdAsync(id);

            if (entite == null)
                return null;

            // Mapper DTO -> Domain
            entite.Name = dto.Name;
            entite.Description = dto.Description;
            entite.Json = dto.Json;
            entite.Template = dto.Template;
            entite.SqlStr = dto.SqlStr;
            entite.SqlPart = dto.SqlPart;

            // Appeler le repository
            var mettreAJour = await _repository.UpdateAsync(entite);

            if (mettreAJour == null)
                return null;

            // Mapper Domain -> DTO Read
            return new RuleNameReadDTO
            {
                Id = mettreAJour.Id,
                Name = mettreAJour.Name,
                Description = mettreAJour.Description,
                Json = mettreAJour.Json,
                Template = mettreAJour.Template,
                SqlStr = mettreAJour.SqlStr,
                SqlPart = mettreAJour.SqlPart
            };
        }
    }
}
