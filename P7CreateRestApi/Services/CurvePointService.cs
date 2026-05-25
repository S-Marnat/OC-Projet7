using P7CreateRestApi.Models;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Services
{
    public class CurvePointService : ICurvePointService
    {
        public Task<CurvePointReadDTO> CreateAsync(CurvePointCreateDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CurvePointReadDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CurvePointReadDTO?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CurvePointReadDTO?> UpdateAsync(int id, CurvePointUpdateDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
