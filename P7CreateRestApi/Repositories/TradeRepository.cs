using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Repositories.Interfaces;

namespace P7CreateRestApi.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        private readonly LocalDbContext _context;

        public TradeRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task<Trade> CreateAsync(Trade trade)
        {
            _context.Trades.Add(trade);
            await _context.SaveChangesAsync();
            return trade;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var trade = await _context.Trades.FindAsync(id);

            if (trade != null)
            {
                _context.Trades.Remove(trade);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Trade>> GetAllAsync()
        {
            return await _context.Trades.ToListAsync();
        }

        public async Task<Trade?> GetByIdAsync(int id)
        {
            return await _context.Trades.FindAsync(id);
        }

        public async Task<Trade> UpdateAsync(Trade trade)
        {
            _context.Trades.Update(trade);
            await _context.SaveChangesAsync();
            return trade;
        }
    }
}
