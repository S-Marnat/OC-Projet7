using Dot.Net.WebApi.Domain;

namespace P7CreateRestApi.Repositories.Interfaces
{
    public interface ICurvePointRepository
    {
        Task<CurvePoint?> GetByIdAsync(int id);
        Task<IEnumerable<CurvePoint>> GetAllAsync();
        Task<CurvePoint> CreateAsync(CurvePoint curvePoint);
        Task<CurvePoint> UpdateAsync(CurvePoint curvePoint);
        Task<bool> DeleteAsync(int id);
    }
}
