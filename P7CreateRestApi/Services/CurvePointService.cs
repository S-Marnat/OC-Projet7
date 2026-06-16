using P7CreateRestApi.Domain;
using P7CreateRestApi.Models;
using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Services
{
    public class CurvePointService : ICurvePointService
    {
        private readonly ICurvePointRepository _repository;

        public CurvePointService(ICurvePointRepository repository)
        {
            _repository = repository;
        }

        public async Task<CurvePointReadDTO> CreateAsync(CurvePointCreateDTO dto)
        {
            // Mapper DTO -> Domain
            var entite = new CurvePoint
            {
                CurveId = dto.CurveId,
                AsOfDate = dto.AsOfDate,
                Term = dto.Term,
                CurvePointValue = dto.CurvePointValue,
                CreationDate = DateTime.UtcNow
            };

            // Appeler le repository
            var creer = await _repository.CreateAsync(entite);

            // Mapper Domain -> DTO Read
            return new CurvePointReadDTO
            {
                Id = creer.Id,
                CurveId = creer.CurveId,
                AsOfDate = creer.AsOfDate,
                Term = creer.Term,
                CurvePointValue = creer.CurvePointValue,
                CreationDate = creer.CreationDate
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Appeler le repository
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CurvePointReadDTO>> GetAllAsync()
        {
            // Appeler le repository
            var entites = await _repository.GetAllAsync();

            // Mapper Domain -> DTO Read
            return entites.Select(e => new CurvePointReadDTO
            {
                Id = e.Id,
                CurveId = e.CurveId,
                AsOfDate = e.AsOfDate,
                Term = e.Term,
                CurvePointValue = e.CurvePointValue,
                CreationDate = e.CreationDate
            });
        }

        public async Task<CurvePointReadDTO?> GetByIdAsync(int id)
        {
            // Appeler le repository
            var entite = await _repository.GetByIdAsync(id);

            if (entite == null)
                return null;

            // Mapper Domain -> DTO Read
            return new CurvePointReadDTO
            {
                Id = entite.Id,
                CurveId = entite.CurveId,
                AsOfDate = entite.AsOfDate,
                Term = entite.Term,
                CurvePointValue = entite.CurvePointValue,
                CreationDate = entite.CreationDate
            };
        }

        public async Task<CurvePointReadDTO?> UpdateAsync(int id, CurvePointUpdateDTO dto)
        {
            // Récupérer l'entité existante
            var entite = await _repository.GetByIdAsync(id);

            if (entite == null)
                return null;

            // Mapper DTO -> Domain
            entite.CurveId = dto.CurveId;
            entite.AsOfDate = dto.AsOfDate;
            entite.Term = dto.Term;
            entite.CurvePointValue = dto.CurvePointValue;

            // Appeler le repository
            var mettreAJour = await _repository.UpdateAsync(entite);

            if (mettreAJour == null)
                return null;

            // Mapper Domain -> DTO Read
            return new CurvePointReadDTO
            {
                Id = mettreAJour.Id,
                CurveId= mettreAJour.CurveId,
                AsOfDate = mettreAJour.AsOfDate,
                Term = mettreAJour.Term,
                CurvePointValue = mettreAJour.CurvePointValue,
                CreationDate = mettreAJour.CreationDate
            };
        }
    }
}
