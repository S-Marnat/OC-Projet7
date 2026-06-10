using Dot.Net.WebApi.Domain;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using P7CreateRestApi.Models;
using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace P7CreateRestApi.Tests.Unit.Services
{
    public class TradeServiceTests
    {
        [Fact]
        public async Task CreateAsync_AppelerRepository1Fois()
        {
            // Arrange
            var mock = new Mock<ITradeRepository>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            // Faux utilisateur
            var fauxUser = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "Faux utilisateur")
                    },
                    "TestAuthType"
                )
            );

            var fauxContext = new DefaultHttpContext
            {
                User = fauxUser
            };

            httpContextAccessorMock.Setup(x => x.HttpContext)
                                   .Returns(fauxContext);

            var dto = new TradeCreateDTO
            {
                Account = "ACC-001",
                AccountType = "Buy",
                BuyQuantity = 1000,
                SellQuantity = 0,
                BuyPrice = 98.50,
                SellPrice = 0,
                Benchmark = "LIBOR",
                TradeDate = new DateTime(2024, 1, 15),
                TradeSecurity = "Bond AAA 2028",
                TradeStatus = "Open",
                Trader = "John Doe",
                Book = "TRADING_BOOK_1",
                DealName = "Bond Purchase 2024",
                DealType = "Buy",
                SourceListId = "SRC-001",
                Side = "Buy"
            };

            var entiteCree = new Trade
            {
                TradeId = 1,
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
                CreationName = "Faux utilisateur",
                RevisionName = "Faux utilisateur",
                RevisionDate = DateTime.UtcNow
            };

            mock.Setup(r => r.CreateAsync(It.IsAny<Trade>()))
                .ReturnsAsync(entiteCree);

            var service = new TradeService(mock.Object, httpContextAccessorMock.Object);

            // Act
            await service.CreateAsync(dto);

            // Assert
            mock.Verify(r => r.CreateAsync(It.IsAny<Trade>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_MappingCorrect()
        {
            // Arrange
            var mock = new Mock<ITradeRepository>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            // Faux utilisateur
            var fauxUser = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[]
                    {
                new Claim(ClaimTypes.Name, "Faux utilisateur")
                    },
                    "TestAuthType"
                )
            );

            var fauxContext = new DefaultHttpContext
            {
                User = fauxUser
            };

            httpContextAccessorMock.Setup(x => x.HttpContext)
                                   .Returns(fauxContext);

            var dto = new TradeCreateDTO
            {
                Account = "ACC-001",
                AccountType = "Buy",
                BuyQuantity = 1000,
                SellQuantity = 0,
                BuyPrice = 98.50,
                SellPrice = 0,
                Benchmark = "LIBOR",
                TradeDate = new DateTime(2024, 1, 15),
                TradeSecurity = "Bond AAA 2028",
                TradeStatus = "Open",
                Trader = "John Doe",
                Book = "TRADING_BOOK_1",
                DealName = "Bond Purchase 2024",
                DealType = "Buy",
                SourceListId = "SRC-001",
                Side = "Buy"
            };

            var entiteCree = new Trade
            {
                TradeId = 1,
                Account = "ACC-001",
                AccountType = "Buy",
                BuyQuantity = 1000,
                SellQuantity = 0,
                BuyPrice = 98.50,
                SellPrice = 0,
                Benchmark = "LIBOR",
                TradeDate = new DateTime(2024, 1, 15),
                TradeSecurity = "Bond AAA 2028",
                TradeStatus = "Open",
                Trader = "John Doe",
                Book = "TRADING_BOOK_1",
                DealName = "Bond Purchase 2024",
                DealType = "Buy",
                SourceListId = "SRC-001",
                Side = "Buy",
                CreationDate = DateTime.UtcNow,
                CreationName = "Faux utilisateur",
                RevisionName = "Faux utilisateur",
                RevisionDate = DateTime.UtcNow
            };

            mock.Setup(r => r.CreateAsync(It.IsAny<Trade>()))
                .ReturnsAsync(entiteCree);

            var service = new TradeService(mock.Object, httpContextAccessorMock.Object);

            // Act
            var resultat = await service.CreateAsync(dto);

            // Assert
            resultat.Should().NotBeNull();
            resultat.TradeId.Should().Be(1);
            resultat.Account.Should().Be("ACC-001");
            resultat.AccountType.Should().Be("Buy");
            resultat.BuyQuantity.Should().Be(1000);
            resultat.SellQuantity.Should().Be(0);
            resultat.BuyPrice.Should().Be(98.50);
            resultat.SellPrice.Should().Be(0);
            resultat.Benchmark.Should().Be("LIBOR");
            resultat.TradeDate.Should().Be(new DateTime(2024, 1, 15));
            resultat.TradeSecurity.Should().Be("Bond AAA 2028");
            resultat.TradeStatus.Should().Be("Open");
            resultat.Trader.Should().Be("John Doe");
            resultat.Book.Should().Be("TRADING_BOOK_1");
            resultat.DealName.Should().Be("Bond Purchase 2024");
            resultat.DealType.Should().Be("Buy");
            resultat.Side.Should().Be("Buy");
            resultat.CreationDate.Should().NotBe(default);
            resultat.CreationName.Should().Be("Faux utilisateur");
            resultat.RevisionName.Should().Be("Faux utilisateur");
            resultat.RevisionDate.Should().NotBe(default);
        }

        [Fact]
        public async Task DeleteAsync_AppelerRepository1Fois()
        {
            // Arrange
            var mock = new Mock<ITradeRepository>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            // Faux utilisateur
            var fauxUser = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[]
                    {
                new Claim(ClaimTypes.Name, "Faux utilisateur")
                    },
                    "TestAuthType"
                )
            );

            var fauxContext = new DefaultHttpContext
            {
                User = fauxUser
            };

            httpContextAccessorMock.Setup(x => x.HttpContext)
                                   .Returns(fauxContext);

            mock.Setup(r => r.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            var service = new TradeService(mock.Object, httpContextAccessorMock.Object);

            // Act
            await service.DeleteAsync(1);

            // Assert
            mock.Verify(r => r.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_AppelerRepository1Fois()
        {
            // Arrange
            var mock = new Mock<ITradeRepository>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            // Faux utilisateur
            var fauxUser = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[]
                    {
                new Claim(ClaimTypes.Name, "Faux utilisateur")
                    },
                    "TestAuthType"
                )
            );

            var fauxContext = new DefaultHttpContext
            {
                User = fauxUser
            };

            httpContextAccessorMock.Setup(x => x.HttpContext)
                                   .Returns(fauxContext);

            mock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Trade>());

            var service = new TradeService(mock.Object, httpContextAccessorMock.Object);

            // Act
            await service.GetAllAsync();

            // Assert
            mock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_MappingCorrect()
        {
            // Arrange
            var mock = new Mock<ITradeRepository>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            // Faux utilisateur
            var fauxUser = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[]
                    {
                new Claim(ClaimTypes.Name, "Faux utilisateur")
                    },
                    "TestAuthType"
                )
            );

            var fauxContext = new DefaultHttpContext
            {
                User = fauxUser
            };

            httpContextAccessorMock.Setup(x => x.HttpContext)
                                   .Returns(fauxContext);

            var entite1 = new Trade
            {
                TradeId = 1,
                Account = "ACC-001",
                AccountType = "Buy",
                BuyQuantity = 1000,
                SellQuantity = 0,
                BuyPrice = 98.50,
                SellPrice = 0,
                Benchmark = "LIBOR",
                TradeDate = new DateTime(2024, 1, 15),
                TradeSecurity = "Bond AAA 2028",
                TradeStatus = "Open",
                Trader = "John Doe",
                Book = "TRADING_BOOK_1",
                DealName = "Bond Purchase 2024",
                DealType = "Buy",
                SourceListId = "SRC-001",
                Side = "Buy",
                CreationDate = DateTime.UtcNow,
                CreationName = "Faux utilisateur",
                RevisionName = "Faux utilisateur",
                RevisionDate = DateTime.UtcNow
            };

            var entite2 = new Trade
            {
                TradeId = 2,
                Account = "ACC-002",
                AccountType = "Sell",
                BuyQuantity = 0,
                SellQuantity = 500,
                BuyPrice = 0,
                SellPrice = 102.75,
                Benchmark = "EURIBOR",
                TradeDate = new DateTime(2024, 2, 15),
                TradeSecurity = "Bond AAA 2028",
                TradeStatus = "Closed",
                Trader = "Sophie Martin",
                Book = "HEDGE_BOOK",
                DealName = "Bond Sale 2030",
                DealType = "Sell",
                SourceListId = "SRC-002",
                Side = "Sell",
                CreationDate = DateTime.UtcNow,
                CreationName = "Faux utilisateur",
                RevisionName = "Faux utilisateur",
                RevisionDate = DateTime.UtcNow
            };

            mock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Trade> { entite1, entite2 });

            var service = new TradeService(mock.Object, httpContextAccessorMock.Object);

            // Act
            var resultat = await service.GetAllAsync();

            // Assert
            resultat.Should().HaveCount(2);

            resultat.First().TradeId.Should().Be(1);
            resultat.Should().NotBeNull();
            resultat.First().TradeId.Should().Be(1);
            resultat.First().Account.Should().Be("ACC-001");
            resultat.First().AccountType.Should().Be("Buy");
            resultat.First().BuyQuantity.Should().Be(1000);
            resultat.First().SellQuantity.Should().Be(0);
            resultat.First().BuyPrice.Should().Be(98.50);
            resultat.First().SellPrice.Should().Be(0);
            resultat.First().Benchmark.Should().Be("LIBOR");
            resultat.First().TradeDate.Should().Be(new DateTime(2024, 1, 15));
            resultat.First().TradeSecurity.Should().Be("Bond AAA 2028");
            resultat.First().TradeStatus.Should().Be("Open");
            resultat.First().Trader.Should().Be("John Doe");
            resultat.First().Book.Should().Be("TRADING_BOOK_1");
            resultat.First().DealName.Should().Be("Bond Purchase 2024");
            resultat.First().DealType.Should().Be("Buy");
            resultat.First().Side.Should().Be("Buy");
            resultat.First().CreationDate.Should().NotBe(default);
            resultat.First().CreationName.Should().Be("Faux utilisateur");
            resultat.First().RevisionName.Should().Be("Faux utilisateur");
            resultat.First().RevisionDate.Should().NotBe(default);

            resultat.Last().TradeId.Should().Be(2);
            resultat.Last().Account.Should().Be("ACC-002");
            resultat.Last().AccountType.Should().Be("Sell");
            resultat.Last().BuyQuantity.Should().Be(0);
            resultat.Last().SellQuantity.Should().Be(500);
            resultat.Last().BuyPrice.Should().Be(0);
            resultat.Last().SellPrice.Should().Be(102.75);
            resultat.Last().Benchmark.Should().Be("EURIBOR");
            resultat.Last().TradeDate.Should().Be(new DateTime(2024, 2, 15));
            resultat.Last().TradeSecurity.Should().Be("Bond AAA 2028");
            resultat.Last().TradeStatus.Should().Be("Closed");
            resultat.Last().Trader.Should().Be("Sophie Martin");
            resultat.Last().Book.Should().Be("HEDGE_BOOK");
            resultat.Last().DealName.Should().Be("Bond Sale 2030");
            resultat.Last().DealType.Should().Be("Sell");
            resultat.Last().Side.Should().Be("Sell");
            resultat.Last().CreationDate.Should().NotBe(default);
            resultat.Last().CreationName.Should().Be("Faux utilisateur");
            resultat.Last().RevisionName.Should().Be("Faux utilisateur");
            resultat.Last().RevisionDate.Should().NotBe(default);
        }

        [Fact]
        public async Task GetByIdAsync_AppelerRepository1Fois()
        {
            // Arrange
            var mock = new Mock<ITradeRepository>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            // Faux utilisateur
            var fauxUser = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[]
                    {
                new Claim(ClaimTypes.Name, "Faux utilisateur")
                    },
                    "TestAuthType"
                )
            );

            var fauxContext = new DefaultHttpContext
            {
                User = fauxUser
            };

            httpContextAccessorMock.Setup(x => x.HttpContext)
                                   .Returns(fauxContext);

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(new Trade());

            var service = new TradeService(mock.Object, httpContextAccessorMock.Object);

            // Act
            await service.GetByIdAsync(1);

            // Assert
            mock.Verify(r => r.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_MappingCorrect()
        {
            // Arrange
            var mock = new Mock<ITradeRepository>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            // Faux utilisateur
            var fauxUser = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[]
                    {
                new Claim(ClaimTypes.Name, "Faux utilisateur")
                    },
                    "TestAuthType"
                )
            );

            var fauxContext = new DefaultHttpContext
            {
                User = fauxUser
            };

            httpContextAccessorMock.Setup(x => x.HttpContext)
                                   .Returns(fauxContext);

            var entite1 = new Trade
            {
                TradeId = 1,
                Account = "ACC-001",
                AccountType = "Buy",
                BuyQuantity = 1000,
                SellQuantity = 0,
                BuyPrice = 98.50,
                SellPrice = 0,
                Benchmark = "LIBOR",
                TradeDate = new DateTime(2024, 1, 15),
                TradeSecurity = "Bond AAA 2028",
                TradeStatus = "Open",
                Trader = "John Doe",
                Book = "TRADING_BOOK_1",
                DealName = "Bond Purchase 2024",
                DealType = "Buy",
                SourceListId = "SRC-001",
                Side = "Buy",
                CreationDate = DateTime.UtcNow,
                CreationName = "Faux utilisateur",
                RevisionName = "Faux utilisateur",
                RevisionDate = DateTime.UtcNow
            };

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(entite1);

            var service = new TradeService(mock.Object, httpContextAccessorMock.Object);

            // Act
            var resultat = await service.GetByIdAsync(1);

            // Assert
            resultat.Should().NotBeNull();

            resultat.TradeId.Should().Be(1);
            resultat.Should().NotBeNull();
            resultat.TradeId.Should().Be(1);
            resultat.Account.Should().Be("ACC-001");
            resultat.AccountType.Should().Be("Buy");
            resultat.BuyQuantity.Should().Be(1000);
            resultat.SellQuantity.Should().Be(0);
            resultat.BuyPrice.Should().Be(98.50);
            resultat.SellPrice.Should().Be(0);
            resultat.Benchmark.Should().Be("LIBOR");
            resultat.TradeDate.Should().Be(new DateTime(2024, 1, 15));
            resultat.TradeSecurity.Should().Be("Bond AAA 2028");
            resultat.TradeStatus.Should().Be("Open");
            resultat.Trader.Should().Be("John Doe");
            resultat.Book.Should().Be("TRADING_BOOK_1");
            resultat.DealName.Should().Be("Bond Purchase 2024");
            resultat.DealType.Should().Be("Buy");
            resultat.Side.Should().Be("Buy");
            resultat.CreationDate.Should().NotBe(default);
            resultat.CreationName.Should().Be("Faux utilisateur");
            resultat.RevisionName.Should().Be("Faux utilisateur");
            resultat.RevisionDate.Should().NotBe(default);
        }

        [Fact]
        public async Task UpdateAsync_AppelerRepository1Fois()
        {
            // Arrange
            var mock = new Mock<ITradeRepository>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            // Faux utilisateur
            var fauxUser = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[]
                    {
                new Claim(ClaimTypes.Name, "Faux utilisateur")
                    },
                    "TestAuthType"
                )
            );

            var fauxContext = new DefaultHttpContext
            {
                User = fauxUser
            };

            httpContextAccessorMock.Setup(x => x.HttpContext)
                                   .Returns(fauxContext);

            var dto = new TradeUpdateDTO
            {
                Account = "ACC-001",
                AccountType = "Buy",
                BuyQuantity = 1000,
                SellQuantity = 0,
                BuyPrice = 98.50,
                SellPrice = 0,
                Benchmark = "LIBOR",
                TradeDate = new DateTime(2024, 1, 15),
                TradeSecurity = "Bond AAA 2028",
                TradeStatus = "Open",
                Trader = "John Doe",
                Book = "TRADING_BOOK_1",
                DealName = "Bond Purchase 2024",
                DealType = "Buy",
                Side = "Buy"
            };

            var entiteMiseAJour = new Trade
            {
                TradeId = 1,
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
                SourceListId = "SRC-001",
                Side = dto.Side,
                CreationDate = DateTime.UtcNow,
                CreationName = "Faux utilisateur",
                RevisionName = "Faux utilisateur",
                RevisionDate = DateTime.UtcNow
            };

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(entiteMiseAJour);

            mock.Setup(r => r.UpdateAsync(It.IsAny<Trade>()))
                .ReturnsAsync(entiteMiseAJour);

            var service = new TradeService(mock.Object, httpContextAccessorMock.Object);

            // Act
            await service.UpdateAsync(1, dto);

            // Assert
            mock.Verify(r => r.UpdateAsync(It.IsAny<Trade>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_MappingCorrect()
        {
            // Arrange
            var mock = new Mock<ITradeRepository>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            // Faux utilisateur
            var fauxUser = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[]
                    {
                new Claim(ClaimTypes.Name, "Faux utilisateur")
                    },
                    "TestAuthType"
                )
            );

            var fauxContext = new DefaultHttpContext
            {
                User = fauxUser
            };

            httpContextAccessorMock.Setup(x => x.HttpContext)
                                   .Returns(fauxContext);

            var dto = new TradeUpdateDTO
            {
                Account = "ACC-001",
                AccountType = "Buy",
                BuyQuantity = 1000,
                SellQuantity = 0,
                BuyPrice = 98.50,
                SellPrice = 0,
                Benchmark = "LIBOR",
                TradeDate = new DateTime(2024, 1, 15),
                TradeSecurity = "Bond AAA 2028",
                TradeStatus = "Open",
                Trader = "John Doe",
                Book = "TRADING_BOOK_1",
                DealName = "Bond Purchase 2024",
                DealType = "Buy",
                Side = "Buy"
            };

            var entiteMiseAJour = new Trade
            {
                TradeId = 1,
                Account = "ACC-001",
                AccountType = "Buy",
                BuyQuantity = 1000,
                SellQuantity = 0,
                BuyPrice = 98.50,
                SellPrice = 0,
                Benchmark = "LIBOR",
                TradeDate = new DateTime(2024, 1, 15),
                TradeSecurity = "Bond AAA 2028",
                TradeStatus = "Open",
                Trader = "John Doe",
                Book = "TRADING_BOOK_1",
                DealName = "Bond Purchase 2024",
                DealType = "Buy",
                SourceListId = "SRC-001",
                Side = "Buy",
                CreationDate = DateTime.UtcNow,
                CreationName = "Faux utilisateur",
                RevisionName = "Faux utilisateur",
                RevisionDate = DateTime.UtcNow
            };

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(entiteMiseAJour);

            mock.Setup(r => r.UpdateAsync(It.IsAny<Trade>()))
                .ReturnsAsync(entiteMiseAJour);

            var service = new TradeService(mock.Object, httpContextAccessorMock.Object);

            // Act
            var resultat = await service.UpdateAsync(1, dto);

            // Assert
            resultat.Should().NotBeNull();
            resultat.Should().NotBeNull();
            resultat.TradeId.Should().Be(1);
            resultat.Account.Should().Be("ACC-001");
            resultat.AccountType.Should().Be("Buy");
            resultat.BuyQuantity.Should().Be(1000);
            resultat.SellQuantity.Should().Be(0);
            resultat.BuyPrice.Should().Be(98.50);
            resultat.SellPrice.Should().Be(0);
            resultat.Benchmark.Should().Be("LIBOR");
            resultat.TradeDate.Should().Be(new DateTime(2024, 1, 15));
            resultat.TradeSecurity.Should().Be("Bond AAA 2028");
            resultat.TradeStatus.Should().Be("Open");
            resultat.Trader.Should().Be("John Doe");
            resultat.Book.Should().Be("TRADING_BOOK_1");
            resultat.DealName.Should().Be("Bond Purchase 2024");
            resultat.DealType.Should().Be("Buy");
            resultat.Side.Should().Be("Buy");
            resultat.CreationDate.Should().NotBe(default);
            resultat.CreationName.Should().Be("Faux utilisateur");
            resultat.RevisionName.Should().Be("Faux utilisateur");
            resultat.RevisionDate.Should().NotBe(default);
        }
    }
}
