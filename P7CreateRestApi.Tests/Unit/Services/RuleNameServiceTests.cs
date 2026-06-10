using Dot.Net.WebApi.Domain;
using FluentAssertions;
using Moq;
using P7CreateRestApi.Models;
using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Tests.Unit.Services
{
    public class RuleNameServiceTests
    {
        [Fact]
        public async Task CreateAsync_AppelerRepository1Fois()
        {
            // Arrange
            var mock = new Mock<IRuleNameRepository>();

            var dto = new RuleNameCreateDTO
            {
                Name = "AccountBalanceCheck",
                Description = "Vérifie que le solde du compte est positif",
                Json = "{\"minBalance\": 0}",
                Template = "Account {{accountId}} has a balance of {{balance}}",
                SqlStr = "SELECT * FROM Accounts WHERE Balance >= 0",
                SqlPart = "WHERE Balance >= 0"
            };

            var entiteCree = new RuleName
            {
                Id = 1,
                Name = dto.Name,
                Description = dto.Description,
                Json = dto.Json,
                Template = dto.Template,
                SqlStr = dto.SqlStr,
                SqlPart = dto.SqlPart
            };

            mock.Setup(r => r.CreateAsync(It.IsAny<RuleName>()))
                .ReturnsAsync(entiteCree);

            var service = new RuleNameService(mock.Object);

            // Act
            await service.CreateAsync(dto);

            // Assert
            mock.Verify(r => r.CreateAsync(It.IsAny<RuleName>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_MappingCorrect()
        {
            // Arrange
            var mock = new Mock<IRuleNameRepository>();

            var dto = new RuleNameCreateDTO
            {
                Name = "AccountBalanceCheck",
                Description = "Vérifie que le solde du compte est positif",
                Json = "{\"minBalance\": 0}",
                Template = "Account {{accountId}} has a balance of {{balance}}",
                SqlStr = "SELECT * FROM Accounts WHERE Balance >= 0",
                SqlPart = "WHERE Balance >= 0"
            };

            var entiteCree = new RuleName
            {
                Id = 1,
                Name = "AccountBalanceCheck",
                Description = "Vérifie que le solde du compte est positif",
                Json = "{\"minBalance\": 0}",
                Template = "Account {{accountId}} has a balance of {{balance}}",
                SqlStr = "SELECT * FROM Accounts WHERE Balance >= 0",
                SqlPart = "WHERE Balance >= 0"
            };

            mock.Setup(r => r.CreateAsync(It.IsAny<RuleName>()))
                .ReturnsAsync(entiteCree);

            var service = new RuleNameService(mock.Object);

            // Act
            var resultat = await service.CreateAsync(dto);

            // Assert
            resultat.Should().NotBeNull();
            resultat.Id.Should().Be(1);
            resultat.Name.Should().Be("AccountBalanceCheck");
            resultat.Description.Should().Be("Vérifie que le solde du compte est positif");
            resultat.Json.Should().Be("{\"minBalance\": 0}");
            resultat.Template.Should().Be("Account {{accountId}} has a balance of {{balance}}");
            resultat.SqlStr.Should().Be("SELECT * FROM Accounts WHERE Balance >= 0");
            resultat.SqlPart.Should().Be("WHERE Balance >= 0");
        }

        [Fact]
        public async Task DeleteAsync_AppelerRepository1Fois()
        {
            // Arrange
            var mock = new Mock<IRuleNameRepository>();

            mock.Setup(r => r.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            var service = new RuleNameService(mock.Object);

            // Act
            await service.DeleteAsync(1);

            // Assert
            mock.Verify(r => r.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_AppelerRepository1Fois()
        {
            // Arrange
            var mock = new Mock<IRuleNameRepository>();

            mock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<RuleName>());

            var service = new RuleNameService(mock.Object);

            // Act
            await service.GetAllAsync();

            // Assert
            mock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_MappingCorrect()
        {
            // Arrange
            var mock = new Mock<IRuleNameRepository>();

            var entite1 = new RuleName
            {
                Id = 1,
                Name = "AccountBalanceCheck",
                Description = "Vérifie que le solde du compte est positif",
                Json = "{\"minBalance\": 0}",
                Template = "Account {{accountId}} has a balance of {{balance}}",
                SqlStr = "SELECT * FROM Accounts WHERE Balance >= 0",
                SqlPart = "WHERE Balance >= 0"
            };

            var entite2 = new RuleName
            {
                Id = 2,
                Name = "HighRiskTrade",
                Description = "Détecte les transactions à haut risque",
                Json = "",
                Template = "",
                SqlStr = "",
                SqlPart = ""
            };

            mock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<RuleName> { entite1, entite2 });

            var service = new RuleNameService(mock.Object);

            // Act
            var resultat = await service.GetAllAsync();

            // Assert
            resultat.Should().HaveCount(2);

            resultat.First().Id.Should().Be(1);
            resultat.First().Name.Should().Be("AccountBalanceCheck");
            resultat.First().Description.Should().Be("Vérifie que le solde du compte est positif");
            resultat.First().Json.Should().Be("{\"minBalance\": 0}");
            resultat.First().Template.Should().Be("Account {{accountId}} has a balance of {{balance}}");
            resultat.First().SqlStr.Should().Be("SELECT * FROM Accounts WHERE Balance >= 0");
            resultat.First().SqlPart.Should().Be("WHERE Balance >= 0");

            resultat.Last().Id.Should().Be(2);
            resultat.Last().Name.Should().Be("HighRiskTrade");
            resultat.Last().Description.Should().Be("Détecte les transactions à haut risque");
            resultat.Last().Json.Should().Be("");
            resultat.Last().Template.Should().Be("");
            resultat.Last().SqlStr.Should().Be("");
            resultat.Last().SqlPart.Should().Be("");
        }

        [Fact]
        public async Task GetByIdAsync_AppelerRepository1Fois()
        {
            // Arrange
            var mock = new Mock<IRuleNameRepository>();

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(new RuleName());

            var service = new RuleNameService(mock.Object);

            // Act
            await service.GetByIdAsync(1);

            // Assert
            mock.Verify(r => r.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_RuleNameInexistant_RetournerNull()
        {
            // Arrange
            var mock = new Mock<IRuleNameRepository>();

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync((RuleName?)null);

            var service = new RuleNameService(mock.Object);

            // Act
            var resultat = await service.GetByIdAsync(1);

            // Assert
            resultat.Should().BeNull();
        }

        [Fact]
        public async Task GetByIdAsync_MappingCorrect()
        {
            // Arrange
            var mock = new Mock<IRuleNameRepository>();

            var entite1 = new RuleName
            {
                Id = 1,
                Name = "AccountBalanceCheck",
                Description = "Vérifie que le solde du compte est positif",
                Json = "{\"minBalance\": 0}",
                Template = "Account {{accountId}} has a balance of {{balance}}",
                SqlStr = "SELECT * FROM Accounts WHERE Balance >= 0",
                SqlPart = "WHERE Balance >= 0"
            };

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(entite1);

            var service = new RuleNameService(mock.Object);

            // Act
            var resultat = await service.GetByIdAsync(1);

            // Assert
            resultat.Should().NotBeNull();

            resultat.Id.Should().Be(1);
            resultat.Name.Should().Be("AccountBalanceCheck");
            resultat.Description.Should().Be("Vérifie que le solde du compte est positif");
            resultat.Json.Should().Be("{\"minBalance\": 0}");
            resultat.Template.Should().Be("Account {{accountId}} has a balance of {{balance}}");
            resultat.SqlStr.Should().Be("SELECT * FROM Accounts WHERE Balance >= 0");
            resultat.SqlPart.Should().Be("WHERE Balance >= 0");
        }

        [Fact]
        public async Task UpdateAsync_AppelerRepository1Fois()
        {
            // Arrange
            var mock = new Mock<IRuleNameRepository>();

            var dto = new RuleNameUpdateDTO
            {
                Name = "AccountBalanceCheck",
                Description = "Vérifie que le solde du compte est positif",
                Json = "{\"minBalance\": 0}",
                Template = "Account {{accountId}} has a balance of {{balance}}",
                SqlStr = "SELECT * FROM Accounts WHERE Balance >= 0",
                SqlPart = "WHERE Balance >= 0"
            };

            var entiteMiseAJour = new RuleName
            {
                Id = 1,
                Name = dto.Name,
                Description = dto.Description,
                Json = dto.Json,
                Template = dto.Template,
                SqlStr = dto.SqlStr,
                SqlPart = dto.SqlPart
            };

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(entiteMiseAJour);

            mock.Setup(r => r.UpdateAsync(It.IsAny<RuleName>()))
                .ReturnsAsync(entiteMiseAJour);

            var service = new RuleNameService(mock.Object);

            // Act
            await service.UpdateAsync(1, dto);

            // Assert
            mock.Verify(r => r.UpdateAsync(It.IsAny<RuleName>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_RuleNameInexistant_RetournerNull()
        {
            // Arrange
            var mock = new Mock<IRuleNameRepository>();

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync((RuleName?)null);

            var service = new RuleNameService(mock.Object);

            // Act
            var resultat = await service.UpdateAsync(1, new RuleNameUpdateDTO());

            // Assert
            resultat.Should().BeNull();
        }

        [Fact]
        public async Task UpdateAsync_UpdateEchoue_RetournerNull()
        {
            // Arrange
            var mock = new Mock<IRuleNameRepository>();

            var dto = new RuleNameUpdateDTO
            {
                Name = "AccountBalanceCheck",
                Description = "Vérifie que le solde du compte est positif",
                Json = "{\"minBalance\": 0}",
                Template = "Account {{accountId}} has a balance of {{balance}}",
                SqlStr = "SELECT * FROM Accounts WHERE Balance >= 0",
                SqlPart = "WHERE Balance >= 0"
            };

            var entiteMiseAJour = new RuleName
            {
                Id = 1,
                Name = "AccountBalanceCheck",
                Description = "Vérifie que le solde du compte est positif",
                Json = "{\"minBalance\": 0}",
                Template = "Account {{accountId}} has a balance of {{balance}}",
                SqlStr = "SELECT * FROM Accounts WHERE Balance >= 0",
                SqlPart = "WHERE Balance >= 0"
            };

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(entiteMiseAJour);

            mock.Setup(r => r.UpdateAsync(It.IsAny<RuleName>()))
                .ReturnsAsync((RuleName?)null);

            var service = new RuleNameService(mock.Object);

            // Act
            var resultat = await service.UpdateAsync(1, dto);

            // Assert
            resultat.Should().BeNull();
        }

        [Fact]
        public async Task UpdateAsync_MappingCorrect()
        {
            // Arrange
            var mock = new Mock<IRuleNameRepository>();

            var dto = new RuleNameUpdateDTO
            {
                Name = "AccountBalanceCheck",
                Description = "Vérifie que le solde du compte est positif",
                Json = "{\"minBalance\": 0}",
                Template = "Account {{accountId}} has a balance of {{balance}}",
                SqlStr = "SELECT * FROM Accounts WHERE Balance >= 0",
                SqlPart = "WHERE Balance >= 0"
            };

            var entiteMiseAJour = new RuleName
            {
                Id = 1,
                Name = "AccountBalanceCheck",
                Description = "Vérifie que le solde du compte est positif",
                Json = "{\"minBalance\": 0}",
                Template = "Account {{accountId}} has a balance of {{balance}}",
                SqlStr = "SELECT * FROM Accounts WHERE Balance >= 0",
                SqlPart = "WHERE Balance >= 0"
            };

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(entiteMiseAJour);

            mock.Setup(r => r.UpdateAsync(It.IsAny<RuleName>()))
                .ReturnsAsync(entiteMiseAJour);

            var service = new RuleNameService(mock.Object);

            // Act
            var resultat = await service.UpdateAsync(1, dto);

            // Assert
            resultat.Should().NotBeNull();
            resultat.Name.Should().Be("AccountBalanceCheck");
            resultat.Description.Should().Be("Vérifie que le solde du compte est positif");
            resultat.Json.Should().Be("{\"minBalance\": 0}");
            resultat.Template.Should().Be("Account {{accountId}} has a balance of {{balance}}");
            resultat.SqlStr.Should().Be("SELECT * FROM Accounts WHERE Balance >= 0");
            resultat.SqlPart.Should().Be("WHERE Balance >= 0");
        }
    }
}
