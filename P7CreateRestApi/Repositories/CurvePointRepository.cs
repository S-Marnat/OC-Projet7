using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Repositories.Interfaces;

namespace P7CreateRestApi.Repositories
{
    public class CurvePointRepository : ICurvePointRepository
    {
        public Task<CurvePoint> CreateAsync(CurvePoint curvePoint)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CurvePoint>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CurvePoint?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CurvePoint> UpdateAsync(CurvePoint curvePoint)
        {
            throw new NotImplementedException();
        }
    }
}
