using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models;
using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Services
{
    public class BidListService : IBidListService
    {
        private readonly IBidListRepository _repository;

        public BidListService(IBidListRepository repository)
        {
           _repository = repository;
        }
        
        public async Task<BidListReadDTO> CreateAsync(BidListCreateDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BidListReadDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BidListReadDTO?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<BidListReadDTO> UpdateAsync(int id, BidListUpdateDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
