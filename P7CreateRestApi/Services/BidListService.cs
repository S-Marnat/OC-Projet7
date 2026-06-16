using P7CreateRestApi.Domain;
using P7CreateRestApi.Models;
using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Services
{
    public class BidListService : IBidListService
    {
        private readonly IBidListRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BidListService(IBidListRepository repository, IHttpContextAccessor httpContextAccessor)
        {
           _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<BidListReadDTO> CreateAsync(BidListCreateDTO dto)
        {
            var userName = GetUserName();

            // Mapper DTO -> Domain
            var entite = new BidList
            {
                Account = dto.Account,
                BidType = dto.BidType,
                BidQuantity = dto.BidQuantity,
                AskQuantity = dto.AskQuantity,
                Bid = dto.Bid,
                Ask = dto.Ask,
                Benchmark = dto.Benchmark,
                BidListDate = dto.BidListDate,
                Commentary = dto.Commentary,
                BidSecurity = dto.BidSecurity,
                BidStatus = dto.BidStatus,
                Trader = dto.Trader,
                Book = dto.Book,
                DealName = dto.DealName,
                DealType = dto.DealType,
                SourceListId = dto.SourceListId,
                Side = dto.Side,
                CreationDate = DateTime.UtcNow,
                CreationName = userName,
                RevisionName = userName,
                RevisionDate = DateTime.UtcNow

            };

            // Appeler le repository
            var creer = await _repository.CreateAsync(entite);

            // Mapper Domain -> DTO Read
            return new BidListReadDTO
            {
                BidListId = creer.BidListId,
                Account = creer.Account,
                BidType = creer.BidType,
                BidQuantity = creer.BidQuantity,
                AskQuantity = creer.AskQuantity,
                Bid = creer.Bid,
                Ask = creer.Ask,
                Benchmark = creer.Benchmark,
                BidListDate = creer.BidListDate,
                Commentary = creer.Commentary,
                BidSecurity = creer.BidSecurity,
                BidStatus = creer.BidStatus,
                Trader = creer.Trader,
                Book = creer.Book,
                CreationName = creer.CreationName,
                CreationDate = creer.CreationDate,
                RevisionName = creer.RevisionName,
                RevisionDate = creer.RevisionDate,
                DealName = creer.DealName,
                DealType = creer.DealType,
                Side = creer.Side
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Appeler le repository
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<BidListReadDTO>> GetAllAsync()
        {
            // Appeler le repository
            var entites = await _repository.GetAllAsync();

            // Mapper Domain -> DTO Read
            return entites.Select(e => new BidListReadDTO
            {
                BidListId = e.BidListId,
                Account = e.Account,
                BidType = e.BidType,
                BidQuantity = e.BidQuantity,
                AskQuantity = e.AskQuantity,
                Bid = e.Bid,
                Ask = e.Ask,
                Benchmark = e.Benchmark,
                BidListDate = e.BidListDate,
                Commentary = e.Commentary,
                BidSecurity = e.BidSecurity,
                BidStatus = e.BidStatus,
                Trader = e.Trader,
                Book = e.Book,
                CreationName = e.CreationName,
                CreationDate = e.CreationDate,
                RevisionName = e.RevisionName,
                RevisionDate = e.RevisionDate,
                DealName = e.DealName,
                DealType = e.DealType,
                Side = e.Side
            });
        }

        public async Task<BidListReadDTO?> GetByIdAsync(int id)
        {
            // Appeler le repository
            var entite = await _repository.GetByIdAsync(id);

            if (entite == null)
                return null;

            // Mapper Domain -> DTO Read
            return new BidListReadDTO
            {
                BidListId = entite.BidListId,
                Account = entite.Account,
                BidType = entite.BidType,
                BidQuantity = entite.BidQuantity,
                AskQuantity = entite.AskQuantity,
                Bid = entite.Bid,
                Ask = entite.Ask,
                Benchmark = entite.Benchmark,
                BidListDate = entite.BidListDate,
                Commentary = entite.Commentary,
                BidSecurity = entite.BidSecurity,
                BidStatus = entite.BidStatus,
                Trader = entite.Trader,
                Book = entite.Book,
                CreationName = entite.CreationName,
                CreationDate = entite.CreationDate,
                RevisionName = entite.RevisionName,
                RevisionDate = entite.RevisionDate,
                DealName = entite.DealName,
                DealType = entite.DealType,
                Side = entite.Side
            };
        }

        public async Task<BidListReadDTO?> UpdateAsync(int id, BidListUpdateDTO dto)
        {
            var userName = GetUserName();

            // Récupérer l'entité existante
            var entite = await _repository.GetByIdAsync(id);

            if (entite == null)
                return null;

            // Mapper DTO -> Domain
            entite.Account = dto.Account;
            entite.BidType = dto.BidType;
            entite.BidQuantity = dto.BidQuantity;
            entite.AskQuantity = dto.AskQuantity;
            entite.Bid = dto.Bid;
            entite.Ask = dto.Ask;
            entite.Benchmark = dto.Benchmark;
            entite.BidListDate = dto.BidListDate;
            entite.Commentary = dto.Commentary;
            entite.BidSecurity = dto.BidSecurity;
            entite.BidStatus = dto.BidStatus;
            entite.Trader = dto.Trader;
            entite.Book = dto.Book;
            entite.DealName = dto.DealName;
            entite.DealType = dto.DealType;
            entite.SourceListId = dto.SourceListId;
            entite.Side = dto.Side;
            entite.RevisionName = userName;
            entite.RevisionDate = DateTime.UtcNow;

            // Appeler le repository
            var mettreAJour = await _repository.UpdateAsync(entite);

            if (mettreAJour == null)
                return null;

            // Mapper Domain -> DTO Read
            return new BidListReadDTO
            {
                BidListId = mettreAJour.BidListId,
                Account = mettreAJour.Account,
                BidType = mettreAJour.BidType,
                BidQuantity = mettreAJour.BidQuantity,
                AskQuantity = mettreAJour.AskQuantity,
                Bid = mettreAJour.Bid,
                Ask = mettreAJour.Ask,
                Benchmark = mettreAJour.Benchmark,
                BidListDate = mettreAJour.BidListDate,
                Commentary = mettreAJour.Commentary,
                BidSecurity = mettreAJour.BidSecurity,
                BidStatus = mettreAJour.BidStatus,
                Trader = mettreAJour.Trader,
                Book = mettreAJour.Book,
                CreationName = mettreAJour.CreationName,
                CreationDate = mettreAJour.CreationDate,
                RevisionName = mettreAJour.RevisionName,
                RevisionDate = mettreAJour.RevisionDate,
                DealName = mettreAJour.DealName,
                DealType = mettreAJour.DealType,
                Side = mettreAJour.Side
            };
        }


        private string GetUserName()
        {
            return _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "Inconnu";
        }

    }
}
