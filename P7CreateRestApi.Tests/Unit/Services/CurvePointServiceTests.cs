using Dot.Net.WebApi.Domain;
using FluentAssertions;
using Moq;
using P7CreateRestApi.Models;
using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Tests.Unit.Services
{
    public class CurvePointServiceTests
    {
        [Fact]
        public async Task CreateAsync_AppelerRepository1Fois()
        {
            // Arrange
            var mock = new Mock<ICurvePointRepository>();

            var dto = new CurvePointCreateDTO
            {
                CurveId = 1,
                AsOfDate = new DateTime(2024, 1, 2),
                Term = 1.0,
                CurvePointValue = 3.15
            };

            var entiteCree = new CurvePoint
            {
                Id = 1,
                CurveId = dto.CurveId,
                AsOfDate =dto.AsOfDate,
                Term = dto.Term,
                CurvePointValue = dto.CurvePointValue,
                CreationDate = DateTime.UtcNow
            };

            mock.Setup(r => r.CreateAsync(It.IsAny<CurvePoint>()))
                .ReturnsAsync(entiteCree);

            var service = new CurvePointService(mock.Object);

            // Act
            await service.CreateAsync(dto);

            // Assert
            mock.Verify(r => r.CreateAsync(It.IsAny<CurvePoint>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_MappingCorrect()
        {
            // Arrange
            var mock = new Mock<ICurvePointRepository>();

            var dto = new CurvePointCreateDTO
            {
                CurveId = 1,
                AsOfDate = new DateTime(2024, 1, 2),
                Term = 1.0,
                CurvePointValue = 3.15
            };

            var entiteCree = new CurvePoint
            {
                Id = 1,
                CurveId = 1,
                AsOfDate = new DateTime(2024, 1, 2),
                Term = 1.0,
                CurvePointValue = 3.15,
                CreationDate = DateTime.UtcNow
            };

            mock.Setup(r => r.CreateAsync(It.IsAny<CurvePoint>()))
                .ReturnsAsync(entiteCree);

            var service = new CurvePointService(mock.Object);

            // Act
            var resultat = await service.CreateAsync(dto);

            // Assert
            resultat.Should().NotBeNull();
            resultat.Id.Should().Be(1);
            resultat.CurveId.Should().Be(1);
            resultat.AsOfDate.Should().Be(new DateTime(2024, 1, 2));
            resultat.Term.Should().Be(1.0);
            resultat.CurvePointValue.Should().Be(3.15);
        }

        [Fact]
        public async Task DeleteAsync_AppelerRepository1Fois()
        {
            // Arrange
            var mock = new Mock<ICurvePointRepository>();

            mock.Setup(r => r.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            var service = new CurvePointService(mock.Object);

            // Act
            await service.DeleteAsync(1);

            // Assert
            mock.Verify(r => r.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_AppelerRepository1Fois()
        {
            // Arrange
            var mock = new Mock<ICurvePointRepository>();

            mock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<CurvePoint>());

            var service = new CurvePointService(mock.Object);

            // Act
            await service.GetAllAsync();

            // Assert
            mock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_MappingCorrect()
        {
            // Arrange
            var mock = new Mock<ICurvePointRepository>();

            var entite1 = new CurvePoint
            {
                Id = 1,
                CurveId = 1,
                AsOfDate = new DateTime(2024, 1, 2),
                Term = 1.0,
                CurvePointValue = 3.15,
                CreationDate = DateTime.UtcNow
            };

            var entite2 = new CurvePoint
            {
                Id = 2,
                CurveId = 2,
                AsOfDate = new DateTime(2025, 2, 3),
                Term = 2.0,
                CurvePointValue = 3.40,
                CreationDate = DateTime.UtcNow
            };

            mock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<CurvePoint> { entite1, entite2 });

            var service = new CurvePointService(mock.Object);

            // Act
            var resultat = await service.GetAllAsync();

            // Assert
            resultat.Should().HaveCount(2);

            resultat.First().Id.Should().Be(1);
            resultat.First().CurveId.Should().Be(1);
            resultat.First().AsOfDate.Should().Be(new DateTime(2024, 1, 2));
            resultat.First().Term.Should().Be(1.0);
            resultat.First().CurvePointValue.Should().Be(3.15);

            resultat.Last().Id.Should().Be(2);
            resultat.Last().CurveId.Should().Be(2);
            resultat.Last().AsOfDate.Should().Be(new DateTime(2025, 2, 3));
            resultat.Last().Term.Should().Be(2.0);
            resultat.Last().CurvePointValue.Should().Be(3.40);
        }

        [Fact]
        public async Task GetByIdAsync_AppelerRepository1Fois()
        {
            // Arrange
            var mock = new Mock<ICurvePointRepository>();

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(new CurvePoint());

            var service = new CurvePointService(mock.Object);

            // Act
            await service.GetByIdAsync(1);

            // Assert
            mock.Verify(r => r.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_CurvePointInexistant_RetournerNull()
        {
            // Arrange
            var mock = new Mock<ICurvePointRepository>();

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync((CurvePoint?)null);

            var service = new CurvePointService(mock.Object);

            // Act
            var resultat = await service.GetByIdAsync(1);

            // Assert
            resultat.Should().BeNull();
        }

        [Fact]
        public async Task GetByIdAsync_MappingCorrect()
        {
            // Arrange
            var mock = new Mock<ICurvePointRepository>();

            var entite1 = new CurvePoint
            {
                Id = 1,
                CurveId = 1,
                AsOfDate = new DateTime(2024, 1, 2),
                Term = 1.0,
                CurvePointValue = 3.15,
                CreationDate = DateTime.UtcNow
            };

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(entite1);

            var service = new CurvePointService(mock.Object);

            // Act
            var resultat = await service.GetByIdAsync(1);

            // Assert
            resultat.Should().NotBeNull();

            resultat.Id.Should().Be(1);
            resultat.CurveId.Should().Be(1);
            resultat.AsOfDate.Should().Be(new DateTime(2024, 1, 2));
            resultat.Term.Should().Be(1.0);
            resultat.CurvePointValue.Should().Be(3.15);
        }

        [Fact]
        public async Task UpdateAsync_AppelerRepository1Fois()
        {
            // Arrange
            var mock = new Mock<ICurvePointRepository>();

            var dto = new CurvePointUpdateDTO
            {
                CurveId = 1,
                AsOfDate = new DateTime(2024, 1, 2),
                Term = 1.0,
                CurvePointValue = 3.15
            };

            var entiteMiseAJour = new CurvePoint
            {
                Id = 1,
                CurveId = dto.CurveId,
                AsOfDate = dto.AsOfDate,
                Term = dto.Term,
                CurvePointValue = dto.CurvePointValue,
                CreationDate = DateTime.UtcNow
            };

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(entiteMiseAJour);

            mock.Setup(r => r.UpdateAsync(It.IsAny<CurvePoint>()))
                .ReturnsAsync(entiteMiseAJour);

            var service = new CurvePointService(mock.Object);

            // Act
            await service.UpdateAsync(1, dto);

            // Assert
            mock.Verify(r => r.UpdateAsync(It.IsAny<CurvePoint>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_CurvePointInexistant_RetournerNull()
        {
            // Arrange
            var mock = new Mock<ICurvePointRepository>();

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync((CurvePoint?)null);

            var service = new CurvePointService(mock.Object);

            // Act
            var resultat = await service.UpdateAsync(1, new CurvePointUpdateDTO());

            // Assert
            resultat.Should().BeNull();
        }

        [Fact]
        public async Task UpdateAsync_UpdateEchoue_RetournerNull()
        {
            // Arrange
            var mock = new Mock<ICurvePointRepository>();

            var dto = new CurvePointUpdateDTO
            {
                CurveId = 1,
                AsOfDate = new DateTime(2024, 1, 2),
                Term = 1.0,
                CurvePointValue = 3.15
            };

            var entiteMiseAJour = new CurvePoint
            {
                Id = 1,
                CurveId = 1,
                AsOfDate = new DateTime(2024, 1, 2),
                Term = 1.0,
                CurvePointValue = 3.15,
                CreationDate = DateTime.UtcNow
            };

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(entiteMiseAJour);

            mock.Setup(r => r.UpdateAsync(It.IsAny<CurvePoint>()))
                .ReturnsAsync((CurvePoint?)null);

            var service = new CurvePointService(mock.Object);

            // Act
            var resultat = await service.UpdateAsync(1, dto);

            // Assert
            resultat.Should().BeNull();
        }

        [Fact]
        public async Task UpdateAsync_MappingCorrect()
        {
            // Arrange
            var mock = new Mock<ICurvePointRepository>();

            var dto = new CurvePointUpdateDTO
            {
                CurveId = 1,
                AsOfDate = new DateTime(2024, 1, 2),
                Term = 1.0,
                CurvePointValue = 3.15
            };

            var entiteMiseAJour = new CurvePoint
            {
                Id = 1,
                CurveId = 1,
                AsOfDate = new DateTime(2024, 1, 2),
                Term = 1.0,
                CurvePointValue = 3.15,
                CreationDate = DateTime.UtcNow
            };

            mock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(entiteMiseAJour);

            mock.Setup(r => r.UpdateAsync(It.IsAny<CurvePoint>()))
                .ReturnsAsync(entiteMiseAJour);

            var service = new CurvePointService(mock.Object);

            // Act
            var resultat = await service.UpdateAsync(1, dto);

            // Assert
            resultat.Should().NotBeNull();
            resultat.Id.Should().Be(1);
            resultat.CurveId.Should().Be(1);
            resultat.AsOfDate.Should().Be(new DateTime(2024, 1, 2));
            resultat.Term.Should().Be(1.0);
            resultat.CurvePointValue.Should().Be(3.15);
        }
    }
}
