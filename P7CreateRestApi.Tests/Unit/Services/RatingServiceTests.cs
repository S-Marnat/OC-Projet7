using Dot.Net.WebApi.Domain;
using FluentAssertions;
using Moq;
using P7CreateRestApi.Models;
using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Tests.Unit.Services
{
    public class RatingServiceTests
    {
        [Fact]
        public async Task CreateAsync_AppelerRepository1Fois()
        {
            // Arrange
            var mock = new Mock<IRatingRepository>();

            var dto = new RatingCreateDTO
            {
                MoodysRating = "Aaa",
                SandPRating = "AAA",
                FitchRating = "AAA"
            };

            // Calcul manuel en fonction des dictionnaires présents dans le service
            double moyenne = (1 + 1 + 1) / 3;
            byte orderNumber = (byte)Math.Round(moyenne);

            var entiteCree = new Rating
            {
                Id = 1,
                MoodysRating = dto.MoodysRating,
                SandPRating = dto.SandPRating,
                FitchRating = dto.FitchRating,
                OrderNumber = orderNumber
            };

            mock.Setup(r => r.CreateAsync(It.IsAny<Rating>()))
                .ReturnsAsync(entiteCree);

            var service = new RatingService(mock.Object);

            // Act
            await service.CreateAsync(dto);

            // Assert
            mock.Verify(r => r.CreateAsync(It.IsAny<Rating>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_MappingCorrect()
        {
            // Arrange
            var mock = new Mock<IRatingRepository>();

            var dto = new RatingCreateDTO
            {
                MoodysRating = "Aaa",
                SandPRating = "AAA",
                FitchRating = "AAA"
            };

            double moyenne = (1 + 1 + 1) / 3;
            byte orderNumber = (byte)Math.Round(moyenne);

            var entiteCree = new Rating
            {
                Id = 1,
                MoodysRating = "Aaa",
                SandPRating = "AAA",
                FitchRating = "AAA",
                OrderNumber = orderNumber
            };

            mock.Setup(r => r.CreateAsync(It.IsAny<Rating>()))
                .ReturnsAsync(entiteCree);

            var service = new RatingService(mock.Object);

            // Act
            var resultat = await service.CreateAsync(dto);

            // Assert
            resultat.Should().NotBeNull();
            resultat.Id.Should().Be(1);
            resultat.MoodysRating.Should().Be("Aaa");
            resultat.SandPRating.Should().Be("AAA");
            resultat.FitchRating.Should().Be("AAA");
            resultat.OrderNumber.Should().Be(1);
        }

        [Fact]
        public async Task DeleteAsync_AppelerRepository1Fois()
        {
            // Arrange
            var mock = new Mock<IRatingRepository>();

            mock.Setup(r => r.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            var service = new RatingService(mock.Object);

            // Act
            await service.DeleteAsync(1);

            // Assert
            mock.Verify(r => r.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_AppelerRepository1Fois()
        {
            // Arrange
            var mock = new Mock<IRatingRepository>();

            mock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Rating>());

            var service = new RatingService(mock.Object);

            // Act
            await service.GetAllAsync();

            // Assert
            mock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_MappingCorrect()
        {
            // Arrange
            var mock = new Mock<IRatingRepository>();

            double moyenne1 = (1 + 1 + 1) / 3;
            byte orderNumber1 = (byte)Math.Round(moyenne1);

            var entite1 = new Rating
            {
                Id = 1,
                MoodysRating = "Aaa",
                SandPRating = "AAA",
                FitchRating = "AAA",
                OrderNumber = orderNumber1
            };

            double moyenne2 = (2 + 2 + 2) / 3;
            byte orderNumber2 = (byte)Math.Round(moyenne2);

            var entite2 = new Rating
            {
                Id = 2,
                MoodysRating = "Aa1",
                SandPRating = "AA+",
                FitchRating = "AA+",
                OrderNumber = orderNumber2
            };

            mock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Rating> { entite1, entite2 });

            var service = new RatingService(mock.Object);

            // Act
            var resultat = await service.GetAllAsync();

            // Assert
            resultat.Should().HaveCount(2);

            resultat.First().Id.Should().Be(1);
            resultat.First().MoodysRating.Should().Be("Aaa");
            resultat.First().SandPRating.Should().Be("AAA");
            resultat.First().FitchRating.Should().Be("AAA");
            resultat.First().OrderNumber.Should().Be(1);

            resultat.Last().Id.Should().Be(2);
            resultat.Last().MoodysRating.Should().Be("Aa1");
            resultat.Last().SandPRating.Should().Be("AA+");
            resultat.Last().FitchRating.Should().Be("AA+");
            resultat.Last().OrderNumber.Should().Be(2);
        }

        [Fact]
        public async Task GetByIdAsync_AppelerRepository1Fois()
        {
            // Arrange
            var mock = new Mock<IRatingRepository>();

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(new Rating());

            var service = new RatingService(mock.Object);

            // Act
            await service.GetByIdAsync(1);

            // Assert
            mock.Verify(r => r.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_RatingInexistant_RetournerNull()
        {
            // Arrange
            var mock = new Mock<IRatingRepository>();

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync((Rating?)null);

            var service = new RatingService(mock.Object);

            // Act
            var resultat = await service.GetByIdAsync(1);

            // Assert
            resultat.Should().BeNull();
        }

        [Fact]
        public async Task GetByIdAsync_MappingCorrect()
        {
            // Arrange
            var mock = new Mock<IRatingRepository>();

            double moyenne = (1 + 1 + 1) / 3;
            byte orderNumber = (byte)Math.Round(moyenne);

            var entite1 = new Rating
            {
                Id = 1,
                MoodysRating = "Aaa",
                SandPRating = "AAA",
                FitchRating = "AAA",
                OrderNumber = orderNumber
            };

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(entite1);

            var service = new RatingService(mock.Object);

            // Act
            var resultat = await service.GetByIdAsync(1);

            // Assert
            resultat.Should().NotBeNull();

            resultat.Id.Should().Be(1);
            resultat.MoodysRating.Should().Be("Aaa");
            resultat.SandPRating.Should().Be("AAA");
            resultat.FitchRating.Should().Be("AAA");
            resultat.OrderNumber.Should().Be(1);
        }

        [Fact]
        public async Task UpdateAsync_AppelerRepository1Fois()
        {
            // Arrange
            var mock = new Mock<IRatingRepository>();

            var dto = new RatingUpdateDTO
            {
                MoodysRating = "Aaa",
                SandPRating = "AAA",
                FitchRating = "AAA"
            };

            double moyenne = (1 + 1 + 1) / 3;
            byte orderNumber = (byte)Math.Round(moyenne);

            var entiteMiseAJour = new Rating
            {
                Id = 1,
                MoodysRating = dto.MoodysRating,
                SandPRating = dto.SandPRating,
                FitchRating = dto.FitchRating,
                OrderNumber = orderNumber
            };

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(entiteMiseAJour);

            mock.Setup(r => r.UpdateAsync(It.IsAny<Rating>()))
                .ReturnsAsync(entiteMiseAJour);

            var service = new RatingService(mock.Object);

            // Act
            await service.UpdateAsync(1, dto);

            // Assert
            mock.Verify(r => r.UpdateAsync(It.IsAny<Rating>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_RatingInexistant_RetournerNull()
        {
            // Arrange
            var mock = new Mock<IRatingRepository>();

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync((Rating?)null);

            var service = new RatingService(mock.Object);

            // Act
            var resultat = await service.UpdateAsync(1, new RatingUpdateDTO());

            // Assert
            resultat.Should().BeNull();
        }

        [Fact]
        public async Task UpdateAsync_UpdateEchoue_RetournerNull()
        {
            // Arrange
            var mock = new Mock<IRatingRepository>();

            var dto = new RatingUpdateDTO
            {
                MoodysRating = "Aaa",
                SandPRating = "AAA",
                FitchRating = "AAA"
            };

            double moyenne = (1 + 1 + 1) / 3;
            byte orderNumber = (byte)Math.Round(moyenne);

            var entiteMiseAJour = new Rating
            {
                Id = 1,
                MoodysRating = "Aaa",
                SandPRating = "AAA",
                FitchRating = "AAA",
                OrderNumber = orderNumber
            };

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(entiteMiseAJour);

            mock.Setup(r => r.UpdateAsync(It.IsAny<Rating>()))
                .ReturnsAsync((Rating?)null);

            var service = new RatingService(mock.Object);

            // Act
            var resultat = await service.UpdateAsync(1, dto);

            // Assert
            resultat.Should().BeNull();
        }

        [Fact]
        public async Task UpdateAsync_MappingCorrect()
        {
            // Arrange
            var mock = new Mock<IRatingRepository>();

            var dto = new RatingUpdateDTO
            {
                MoodysRating = "Aaa",
                SandPRating = "AAA",
                FitchRating = "AAA"
            };

            double moyenne = (1 + 1 + 1) / 3;
            byte orderNumber = (byte)Math.Round(moyenne);

            var entiteMiseAJour = new Rating
            {
                Id = 1,
                MoodysRating = "Aaa",
                SandPRating = "AAA",
                FitchRating = "AAA",
                OrderNumber = orderNumber
            };

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(entiteMiseAJour);

            mock.Setup(r => r.UpdateAsync(It.IsAny<Rating>()))
                .ReturnsAsync(entiteMiseAJour);

            var service = new RatingService(mock.Object);

            // Act
            var resultat = await service.UpdateAsync(1, dto);

            // Assert
            resultat.Should().NotBeNull();
            resultat.Id.Should().Be(1);
            resultat.MoodysRating.Should().Be("Aaa");
            resultat.SandPRating.Should().Be("AAA");
            resultat.FitchRating.Should().Be("AAA");
            resultat.OrderNumber.Should().Be(1);
        }
    }
}
