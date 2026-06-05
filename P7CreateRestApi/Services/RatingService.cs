using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models;
using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _repository;

        public RatingService(IRatingRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<RatingReadDTO> CreateAsync(RatingCreateDTO dto)
        {
            // Traitement des données
            byte scoreMoody = ScoresMoodys[dto.MoodysRating];
            byte scoreSP = ScoresSPAndFitch[dto.SandPRating];
            byte scoreFitch = ScoresSPAndFitch[dto.FitchRating];
            byte orderNumber = CalculScoreRating(scoreMoody, scoreSP, scoreFitch);

            // Mapper DTO -> Domain
            var entite = new Rating
            {
                MoodysRating = dto.MoodysRating,
                SandPRating = dto.SandPRating,
                FitchRating = dto.FitchRating,
                OrderNumber = orderNumber
            };

            // Appeler le repository
            var creer = await _repository.CreateAsync(entite);

            // Mapper Domain -> DTO Read
            return new RatingReadDTO
            {
                Id = creer.Id,
                MoodysRating = creer.MoodysRating,
                SandPRating = creer.SandPRating,
                FitchRating = creer.FitchRating,
                OrderNumber = creer.OrderNumber
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Appeler le repository
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<RatingReadDTO>> GetAllAsync()
        {
            // Appeler le repository
            var entites = await _repository.GetAllAsync();

            // Mapper Domain -> DTO Read
            return entites.Select(e => new RatingReadDTO
            {
                Id = e.Id,
                MoodysRating = e.MoodysRating,
                SandPRating = e.SandPRating,
                FitchRating = e.FitchRating,
                OrderNumber = e.OrderNumber
            });
        }

        public async Task<RatingReadDTO?> GetByIdAsync(int id)
        {
            // Appeler le repository
            var entite = await _repository.GetByIdAsync(id);

            if (entite == null)
                return null;

            // Mapper Domain -> DTO Read
            return new RatingReadDTO
            {
                Id = entite.Id,
                MoodysRating = entite.MoodysRating,
                SandPRating = entite.SandPRating,
                FitchRating = entite.FitchRating,
                OrderNumber = entite.OrderNumber
            };
        }

        public async Task<RatingReadDTO?> UpdateAsync(int id, RatingUpdateDTO dto)
        {
            // Récupérer l'entité existante
            var entite = await _repository.GetByIdAsync(id);

            if (entite == null)
                return null;

            // Traitement des données
            byte scoreMoody = ScoresMoodys[dto.MoodysRating];
            byte scoreSP = ScoresSPAndFitch[dto.SandPRating];
            byte scoreFitch = ScoresSPAndFitch[dto.FitchRating];
            byte orderNumber = CalculScoreRating(scoreMoody, scoreSP, scoreFitch);

            // Mapper DTO -> Domain
            entite.MoodysRating = dto.MoodysRating;
            entite.SandPRating = dto.SandPRating;
            entite.FitchRating = dto.FitchRating;
            entite.OrderNumber = orderNumber;

            // Appeler le repository
            var mettreAJour = await _repository.UpdateAsync(entite);

            // Mapper Domain -> DTO Read
            return new RatingReadDTO
            {
                Id = mettreAJour.Id,
                MoodysRating = mettreAJour.MoodysRating,
                SandPRating = mettreAJour.SandPRating,
                FitchRating = mettreAJour.FitchRating,
                OrderNumber = mettreAJour.OrderNumber
            };
        }


        private static readonly Dictionary<string, byte> ScoresMoodys = new()
        {
            ["Aaa"] = 1,
            ["Aa1"] = 2,
            ["Aa2"] = 3,
            ["Aa3"] = 4,
            ["A1"] = 5,
            ["A2"] = 6,
            ["A3"] = 7,
            ["Baa1"] = 8,
            ["Baa2"] = 9,
            ["Baa3"] = 10,
            ["Ba1"] = 11,
            ["Ba2"] = 12,
            ["Ba3"] = 13,
            ["B1"] = 14,
            ["B2"] = 15,
            ["B3"] = 16,
            ["Caa"] = 17,
            ["Ca"] = 18,
            ["C"] = 19,
            ["P-1"] = 20,
            ["P-2"] = 21,
            ["P-3"] = 22,
            ["NP"] = 23
        };

        private static readonly Dictionary<string, byte> ScoresSPAndFitch = new()
        {
            ["AAA"] = 1,
            ["AA+"] = 2,
            ["AA"] = 3,
            ["AA-"] = 4,
            ["A+"] = 5,
            ["A"] = 6,
            ["A-"] = 7,
            ["BBB+"] = 8,
            ["BBB"] = 9,
            ["BBB-"] = 10,
            ["BB+"] = 11,
            ["BB"] = 12,
            ["BB-"] = 13,
            ["B+"] = 14,
            ["B"] = 15,
            ["B-"] = 16,
            ["CCC+"] = 17,
            ["CCC"] = 18,
            ["CCC-"] = 19,
            ["CC"] = 20,
            ["C"] = 21,
            ["D"] = 22,
            ["SD"] = 23,
            ["RD"] = 23
        };

        private byte CalculScoreRating(double moodys, double sandP, double fitch)
        {
            var scores = new List<double> { moodys, sandP, fitch };
            double moyenne = scores.Average();

            return (byte)Math.Round(moyenne);
        }

    }
}
