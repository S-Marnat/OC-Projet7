using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Repositories.Interfaces;

namespace P7CreateRestApi.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly LocalDbContext _context;

        public RatingRepository(LocalDbContext context)
        {
            _context = context;
        }
        
        public async Task<Rating> CreateAsync(Rating rating)
        {
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
            return rating;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);

            if (rating != null)
            {
                _context.Ratings.Remove(rating);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Rating>> GetAllAsync()
        {
            return await _context.Ratings.ToListAsync();
        }

        public async Task<Rating?> GetByIdAsync(int id)
        {
            return await _context.Ratings.FindAsync(id);
        }

        public async Task<Rating> UpdateAsync(Rating rating)
        {
            _context.Ratings.Update(rating);
            await _context.SaveChangesAsync();
            return rating;
        }
    }
}
