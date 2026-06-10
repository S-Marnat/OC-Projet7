using Dot.Net.WebApi.Domain;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using P7CreateRestApi.Models;
using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Services;
using System.Security.Claims;

namespace P7CreateRestApi.Tests.Unit.Services
{
    public class BidListServiceTests
    {
        [Fact]
        public async Task CreateAsync_AppelerRepository1Fois()
        {
            // Arrange
            var mock = new Mock<IBidListRepository>();
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

            var dto = new BidListCreateDTO
            {
                Account = "ClientA",
                BidType = "Type1",
                BidQuantity = 100,
                AskQuantity = 120,
                Bid = 10.5,
                Ask = 11.0,
                Benchmark = "LIBOR",
                BidListDate = new DateTime(2024, 1, 10),
                Commentary = "Initial bid",
                BidSecurity = "Bond A",
                BidStatus = "Open",
                Trader = "Trader1",
                Book = "Book1",
                DealName = "DealA",
                DealType = "Spot",
                SourceListId = "Source1",
                Side = "Buy"
            };

            var entiteCree = new BidList
            {
                BidListId = 1,
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
                CreationName = "Faux utilisateur",
                RevisionName = "Faux utilisateur",
                RevisionDate = DateTime.UtcNow
            };

            mock.Setup(r => r.CreateAsync(It.IsAny<BidList>()))
                .ReturnsAsync(entiteCree);

            var service = new BidListService(mock.Object, httpContextAccessorMock.Object);

            // Act
            await service.CreateAsync(dto);

            // Assert
            mock.Verify(r => r.CreateAsync(It.IsAny<BidList>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_MappingCorrect()
        {
            // Arrange
            var mock = new Mock<IBidListRepository>();
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

            var dto = new BidListCreateDTO
            {
                Account = "ClientA",
                BidType = "Type1",
                BidQuantity = 100,
                AskQuantity = 120,
                Bid = 10.5,
                Ask = 11.0,
                Benchmark = "LIBOR",
                BidListDate = new DateTime(2024, 1, 10),
                Commentary = "Initial bid",
                BidSecurity = "Bond A",
                BidStatus = "Open",
                Trader = "Trader1",
                Book = "Book1",
                DealName = "DealA",
                DealType = "Spot",
                SourceListId = "Source1",
                Side = "Buy"
            };

            var entiteCree = new BidList
            {
                BidListId = 1,
                Account = "ClientA",
                BidType = "Type1",
                BidQuantity = 100,
                AskQuantity = 120,
                Bid = 10.5,
                Ask = 11.0,
                Benchmark = "LIBOR",
                BidListDate = new DateTime(2024, 1, 10),
                Commentary = "Initial bid",
                BidSecurity = "Bond A",
                BidStatus = "Open",
                Trader = "Trader1",
                Book = "Book1",
                DealName = "DealA",
                DealType = "Spot",
                SourceListId = "Source1",
                Side = "Buy",
                CreationDate = DateTime.UtcNow,
                CreationName = "Faux utilisateur",
                RevisionName = "Faux utilisateur",
                RevisionDate = DateTime.UtcNow
            };

            mock.Setup(r => r.CreateAsync(It.IsAny<BidList>()))
                .ReturnsAsync(entiteCree);

            var service = new BidListService(mock.Object, httpContextAccessorMock.Object);

            // Act
            var resultat = await service.CreateAsync(dto);

            // Assert
            resultat.Should().NotBeNull();
            resultat.BidListId.Should().Be(1);
            resultat.Account.Should().Be("ClientA");
            resultat.BidType.Should().Be("Type1");
            resultat.BidQuantity.Should().Be(100);
            resultat.AskQuantity.Should().Be(120);
            resultat.Bid.Should().Be(10.5);
            resultat.Ask.Should().Be(11.0);
            resultat.Benchmark.Should().Be("LIBOR");
            resultat.BidListDate.Should().Be(new DateTime(2024, 1, 10));
            resultat.Commentary.Should().Be("Initial bid");
            resultat.BidSecurity.Should().Be("Bond A");
            resultat.BidStatus.Should().Be("Open");
            resultat.Trader.Should().Be("Trader1");
            resultat.Book.Should().Be("Book1");
            resultat.DealName.Should().Be("DealA");
            resultat.DealType.Should().Be("Spot");
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
            var mock = new Mock<IBidListRepository>();
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

            var service = new BidListService(mock.Object, httpContextAccessorMock.Object);

            // Act
            await service.DeleteAsync(1);

            // Assert
            mock.Verify(r => r.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_AppelerRepository1Fois()
        {
            // Arrange
            var mock = new Mock<IBidListRepository>();
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
                .ReturnsAsync(new List<BidList>());

            var service = new BidListService(mock.Object, httpContextAccessorMock.Object);

            // Act
            await service.GetAllAsync();

            // Assert
            mock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_MappingCorrect()
        {
            // Arrange
            var mock = new Mock<IBidListRepository>();
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

            var entite1 = new BidList
            {
                BidListId = 1,
                Account = "ClientA",
                BidType = "Type1",
                BidQuantity = 100,
                AskQuantity = 120,
                Bid = 10.5,
                Ask = 11.0,
                Benchmark = "LIBOR",
                BidListDate = new DateTime(2024, 1, 10),
                Commentary = "Initial bid",
                BidSecurity = "Bond A",
                BidStatus = "Open",
                Trader = "Trader1",
                Book = "Book1",
                DealName = "DealA",
                DealType = "Spot",
                SourceListId = "Source1",
                Side = "Buy",
                CreationDate = DateTime.UtcNow,
                CreationName = "Faux utilisateur",
                RevisionName = "Faux utilisateur",
                RevisionDate = DateTime.UtcNow
            };

            var entite2 = new BidList
            {
                BidListId = 2,
                Account = "ClientB",
                BidType = "Type2",
                BidQuantity = 200,
                AskQuantity = 210,
                Bid = 20.0,
                Ask = 21.0,
                Benchmark = "EURIBOR",
                BidListDate = new DateTime(2024, 2, 15),
                Commentary = "Urgent request",
                BidSecurity = "Bond B",
                BidStatus = "Pending",
                Trader = "Trader2",
                Book = "Book2",
                DealName = "DealB",
                DealType = "Forward",
                SourceListId = "Source2",
                Side = "Sell",
                CreationDate = DateTime.UtcNow,
                CreationName = "Faux utilisateur",
                RevisionName = "Faux utilisateur",
                RevisionDate = DateTime.UtcNow
            };

            mock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<BidList> { entite1, entite2 });

            var service = new BidListService(mock.Object, httpContextAccessorMock.Object);

            // Act
            var resultat = await service.GetAllAsync();

            // Assert
            resultat.Should().HaveCount(2);

            resultat.First().BidListId.Should().Be(1);
            resultat.Should().NotBeNull();
            resultat.First().BidListId.Should().Be(1);
            resultat.First().Account.Should().Be("ClientA");
            resultat.First().BidType.Should().Be("Type1");
            resultat.First().BidQuantity.Should().Be(100);
            resultat.First().AskQuantity.Should().Be(120);
            resultat.First().Bid.Should().Be(10.5);
            resultat.First().Ask.Should().Be(11.0);
            resultat.First().Benchmark.Should().Be("LIBOR");
            resultat.First().BidListDate.Should().Be(new DateTime(2024, 1, 10));
            resultat.First().Commentary.Should().Be("Initial bid");
            resultat.First().BidSecurity.Should().Be("Bond A");
            resultat.First().BidStatus.Should().Be("Open");
            resultat.First().Trader.Should().Be("Trader1");
            resultat.First().Book.Should().Be("Book1");
            resultat.First().DealName.Should().Be("DealA");
            resultat.First().DealType.Should().Be("Spot");
            resultat.First().Side.Should().Be("Buy");
            resultat.First().CreationDate.Should().NotBe(default);
            resultat.First().CreationName.Should().Be("Faux utilisateur");
            resultat.First().RevisionName.Should().Be("Faux utilisateur");
            resultat.First().RevisionDate.Should().NotBe(default);

            resultat.Last().BidListId.Should().Be(2);
            resultat.Last().Account.Should().Be("ClientB");
            resultat.Last().BidType.Should().Be("Type2");
            resultat.Last().BidQuantity.Should().Be(200);
            resultat.Last().AskQuantity.Should().Be(210);
            resultat.Last().Bid.Should().Be(20.0);
            resultat.Last().Ask.Should().Be(21.0);
            resultat.Last().Benchmark.Should().Be("EURIBOR");
            resultat.Last().BidListDate.Should().Be(new DateTime(2024, 2, 15));
            resultat.Last().Commentary.Should().Be("Urgent request");
            resultat.Last().BidSecurity.Should().Be("Bond B");
            resultat.Last().BidStatus.Should().Be("Pending");
            resultat.Last().Trader.Should().Be("Trader2");
            resultat.Last().Book.Should().Be("Book2");
            resultat.Last().DealName.Should().Be("DealB");
            resultat.Last().DealType.Should().Be("Forward");
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
            var mock = new Mock<IBidListRepository>();
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
                .ReturnsAsync(new BidList());

            var service = new BidListService(mock.Object, httpContextAccessorMock.Object);

            // Act
            await service.GetByIdAsync(1);

            // Assert
            mock.Verify(r => r.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_MappingCorrect()
        {
            // Arrange
            var mock = new Mock<IBidListRepository>();
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

            var entite1 = new BidList
            {
                BidListId = 1,
                Account = "ClientA",
                BidType = "Type1",
                BidQuantity = 100,
                AskQuantity = 120,
                Bid = 10.5,
                Ask = 11.0,
                Benchmark = "LIBOR",
                BidListDate = new DateTime(2024, 1, 10),
                Commentary = "Initial bid",
                BidSecurity = "Bond A",
                BidStatus = "Open",
                Trader = "Trader1",
                Book = "Book1",
                DealName = "DealA",
                DealType = "Spot",
                SourceListId = "Source1",
                Side = "Buy",
                CreationDate = DateTime.UtcNow,
                CreationName = "Faux utilisateur",
                RevisionName = "Faux utilisateur",
                RevisionDate = DateTime.UtcNow
            };

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(entite1);

            var service = new BidListService(mock.Object, httpContextAccessorMock.Object);

            // Act
            var resultat = await service.GetByIdAsync(1);

            // Assert
            resultat.Should().NotBeNull();

            resultat.BidListId.Should().Be(1);
            resultat.Should().NotBeNull();
            resultat.BidListId.Should().Be(1);
            resultat.Account.Should().Be("ClientA");
            resultat.BidType.Should().Be("Type1");
            resultat.BidQuantity.Should().Be(100);
            resultat.AskQuantity.Should().Be(120);
            resultat.Bid.Should().Be(10.5);
            resultat.Ask.Should().Be(11.0);
            resultat.Benchmark.Should().Be("LIBOR");
            resultat.BidListDate.Should().Be(new DateTime(2024, 1, 10));
            resultat.Commentary.Should().Be("Initial bid");
            resultat.BidSecurity.Should().Be("Bond A");
            resultat.BidStatus.Should().Be("Open");
            resultat.Trader.Should().Be("Trader1");
            resultat.Book.Should().Be("Book1");
            resultat.DealName.Should().Be("DealA");
            resultat.DealType.Should().Be("Spot");
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
            var mock = new Mock<IBidListRepository>();
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

            var dto = new BidListUpdateDTO
            {
                Account = "ClientA",
                BidType = "Type1",
                BidQuantity = 100,
                AskQuantity = 120,
                Bid = 10.5,
                Ask = 11.0,
                Benchmark = "LIBOR",
                BidListDate = new DateTime(2024, 1, 10),
                Commentary = "Initial bid",
                BidSecurity = "Bond A",
                BidStatus = "Open",
                Trader = "Trader1",
                Book = "Book1",
                DealName = "DealA",
                DealType = "Spot",
                Side = "Buy"
            };

            var entiteMiseAJour = new BidList
            {
                BidListId = 1,
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
                SourceListId = "Source1",
                Side = dto.Side,
                CreationDate = DateTime.UtcNow,
                CreationName = "Faux utilisateur",
                RevisionName = "Faux utilisateur",
                RevisionDate = DateTime.UtcNow
            };

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(entiteMiseAJour);

            mock.Setup(r => r.UpdateAsync(It.IsAny<BidList>()))
                .ReturnsAsync(entiteMiseAJour);

            var service = new BidListService(mock.Object, httpContextAccessorMock.Object);

            // Act
            await service.UpdateAsync(1, dto);

            // Assert
            mock.Verify(r => r.UpdateAsync(It.IsAny<BidList>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_MappingCorrect()
        {
            // Arrange
            var mock = new Mock<IBidListRepository>();
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

            var dto = new BidListUpdateDTO
            {
                Account = "ClientA",
                BidType = "Type1",
                BidQuantity = 100,
                AskQuantity = 120,
                Bid = 10.5,
                Ask = 11.0,
                Benchmark = "LIBOR",
                BidListDate = new DateTime(2024, 1, 10),
                Commentary = "Initial bid",
                BidSecurity = "Bond A",
                BidStatus = "Open",
                Trader = "Trader1",
                Book = "Book1",
                DealName = "DealA",
                DealType = "Spot",
                Side = "Buy"
            };

            var entiteMiseAJour = new BidList
            {
                BidListId = 1,
                Account = "ClientA",
                BidType = "Type1",
                BidQuantity = 100,
                AskQuantity = 120,
                Bid = 10.5,
                Ask = 11.0,
                Benchmark = "LIBOR",
                BidListDate = new DateTime(2024, 1, 10),
                Commentary = "Initial bid",
                BidSecurity = "Bond A",
                BidStatus = "Open",
                Trader = "Trader1",
                Book = "Book1",
                DealName = "DealA",
                DealType = "Spot",
                SourceListId = "Source1",
                Side = "Buy",
                CreationDate = DateTime.UtcNow,
                CreationName = "Faux utilisateur",
                RevisionName = "Faux utilisateur",
                RevisionDate = DateTime.UtcNow
            };

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(entiteMiseAJour);

            mock.Setup(r => r.UpdateAsync(It.IsAny<BidList>()))
                .ReturnsAsync(entiteMiseAJour);

            var service = new BidListService(mock.Object, httpContextAccessorMock.Object);

            // Act
            var resultat = await service.UpdateAsync(1, dto);

            // Assert
            resultat.Should().NotBeNull();
            resultat.Should().NotBeNull();
            resultat.BidListId.Should().Be(1);
            resultat.Account.Should().Be("ClientA");
            resultat.BidType.Should().Be("Type1");
            resultat.BidQuantity.Should().Be(100);
            resultat.AskQuantity.Should().Be(120);
            resultat.Bid.Should().Be(10.5);
            resultat.Ask.Should().Be(11.0);
            resultat.Benchmark.Should().Be("LIBOR");
            resultat.BidListDate.Should().Be(new DateTime(2024, 1, 10));
            resultat.Commentary.Should().Be("Initial bid");
            resultat.BidSecurity.Should().Be("Bond A");
            resultat.BidStatus.Should().Be("Open");
            resultat.Trader.Should().Be("Trader1");
            resultat.Book.Should().Be("Book1");
            resultat.DealName.Should().Be("DealA");
            resultat.DealType.Should().Be("Spot");
            resultat.Side.Should().Be("Buy");
            resultat.CreationDate.Should().NotBe(default);
            resultat.CreationName.Should().Be("Faux utilisateur");
            resultat.RevisionName.Should().Be("Faux utilisateur");
            resultat.RevisionDate.Should().NotBe(default);
        }
    }
}
