using P7CreateRestApi.Models;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Services
{
    public class RatingService : IRatingService
    {
        public Task<RatingReadDTO> CreateAsync(RatingCreateDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RatingReadDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RatingReadDTO?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<RatingReadDTO?> UpdateAsync(int id, RatingUpdateDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
