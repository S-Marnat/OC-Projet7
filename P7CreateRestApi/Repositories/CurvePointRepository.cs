using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Repositories.Interfaces;

namespace P7CreateRestApi.Repositories
{
    public class CurvePointRepository : ICurvePointRepository
    {
        private readonly LocalDbContext _context;

        public CurvePointRepository(LocalDbContext context)
        {
            _context = context;
        }
        
        public async Task<CurvePoint> CreateAsync(CurvePoint curvePoint)
        {
            _context.CurvePoints.Add(curvePoint);
            await _context.SaveChangesAsync();
            return curvePoint;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var curvePoint = await _context.CurvePoints.FindAsync(id);

            if(curvePoint != null)
            {
                _context.CurvePoints.Remove(curvePoint);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<CurvePoint>> GetAllAsync()
        {
            return await _context.CurvePoints.ToListAsync();
        }

        public async Task<CurvePoint?> GetByIdAsync(int id)
        {
            return await _context.CurvePoints.FindAsync(id);
        }

        public async Task<CurvePoint> UpdateAsync(CurvePoint curvePoint)
        {
            _context.CurvePoints.Update(curvePoint);
            await _context.SaveChangesAsync();
            return curvePoint;
        }
    }
}
