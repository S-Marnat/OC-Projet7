using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models;
using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Services
{
    public class TradeService : ITradeService
    {
        private readonly ITradeRepository _repository;

        public TradeService(ITradeRepository repository)
        {
            _repository = repository;
        }

        public async Task<TradeReadDTO> CreateAsync(TradeCreateDTO dto)
        {
            // Mapper DTO -> Domain
            var entite = new Trade
            {
                Account = dto.Account,
                AccountType = dto.AccountType,
                BuyQuantity = dto.BuyQuantity,
                SellQuantity = dto.SellQuantity,
                BuyPrice = dto.BuyPrice,
                SellPrice = dto.SellPrice,
                Benchmark = dto.Benchmark,
                TradeDate = dto.TradeDate,
                TradeSecurity = dto.TradeSecurity,
                TradeStatus = dto.TradeStatus,
                Trader = dto.Trader,
                Book = dto.Book,
                DealName = dto.DealName,
                DealType = dto.DealType,
                SourceListId = dto.SourceListId,
                Side = dto.Side,
                CreationDate = DateTime.UtcNow,
                CreationName = "user", // A implémenter lors du développement de l'authentification
                RevisionName = "user", // A implémenter lors du développement de l'authentification
                RevisionDate = DateTime.UtcNow

            };

            // Appeler le repository
            var creer = await _repository.CreateAsync(entite);

            // Mapper Domain -> DTO Read
            return new TradeReadDTO
            {
                TradeId = creer.TradeId,
                Account = creer.Account,
                AccountType = creer.AccountType,
                BuyQuantity = creer.BuyQuantity,
                SellQuantity = creer.SellQuantity,
                BuyPrice = creer.BuyPrice,
                SellPrice = creer.SellPrice,
                Benchmark = creer.Benchmark,
                TradeDate = creer.TradeDate,
                TradeSecurity = creer.TradeSecurity,
                TradeStatus = creer.TradeStatus,
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

        public async Task<IEnumerable<TradeReadDTO>> GetAllAsync()
        {
            // Appeler le repository
            var entites = await _repository.GetAllAsync();

            // Mapper Domain -> DTO Read
            return entites.Select(e => new TradeReadDTO
            {
                TradeId = e.TradeId,
                Account = e.Account,
                AccountType = e.AccountType,
                BuyQuantity = e.BuyQuantity,
                SellQuantity = e.SellQuantity,
                BuyPrice = e.BuyPrice,
                SellPrice = e.SellPrice,
                Benchmark = e.Benchmark,
                TradeDate = e.TradeDate,
                TradeSecurity = e.TradeSecurity,
                TradeStatus = e.TradeStatus,
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

        public async Task<TradeReadDTO?> GetByIdAsync(int id)
        {
            // Appeeler le repository
            var entite = await _repository.GetByIdAsync(id);

            if (entite == null)
                return null;

            // Mapper Domain -> DTO Read
            return new TradeReadDTO
            {
                TradeId = entite.TradeId,
                Account = entite.Account,
                AccountType = entite.AccountType,
                BuyQuantity = entite.BuyQuantity,
                SellQuantity = entite.SellQuantity,
                BuyPrice = entite.BuyPrice,
                SellPrice = entite.SellPrice,
                Benchmark = entite.Benchmark,
                TradeDate = entite.TradeDate,
                TradeSecurity = entite.TradeSecurity,
                TradeStatus = entite.TradeStatus,
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

        public async Task<TradeReadDTO?> UpdateAsync(int id, TradeUpdateDTO dto)
        {
            // Récupérer l'entité existante
            var entite = await _repository.GetByIdAsync(id);

            if (entite == null)
                return null;

            // Mapper DTO -> Domain
            entite.Account = dto.Account;
            entite.AccountType = dto.AccountType;
            entite.BuyQuantity = dto.BuyQuantity;
            entite.SellQuantity = dto.SellQuantity;
            entite.BuyPrice = dto.BuyPrice;
            entite.SellPrice = dto.SellPrice;
            entite.Benchmark = dto.Benchmark;
            entite.TradeDate = dto.TradeDate;
            entite.TradeSecurity = dto.TradeSecurity;
            entite.TradeStatus = dto.TradeStatus;
            entite.Trader = dto.Trader;
            entite.Book = dto.Book;
            entite.DealName = dto.DealName;
            entite.DealType = dto.DealType;
            entite.Side = dto.Side;
            entite.RevisionName = "user"; // A implémenter lors du développement de l'authentification;
            entite.RevisionDate = DateTime.UtcNow;

            // Appeler le repository
            var mettreAJour = await _repository.UpdateAsync(entite);

            // Mapper Domain -> DTO Read
            return new TradeReadDTO
            {
                TradeId = mettreAJour.TradeId,
                Account = mettreAJour.Account,
                AccountType = mettreAJour.AccountType,
                BuyQuantity = mettreAJour.BuyQuantity,
                SellQuantity = mettreAJour.SellQuantity,
                BuyPrice = mettreAJour.BuyPrice,
                SellPrice = mettreAJour.SellPrice,
                Benchmark = mettreAJour.Benchmark,
                TradeDate = mettreAJour.TradeDate,
                TradeSecurity = mettreAJour.TradeSecurity,
                TradeStatus = mettreAJour.TradeStatus,
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
    }
}
