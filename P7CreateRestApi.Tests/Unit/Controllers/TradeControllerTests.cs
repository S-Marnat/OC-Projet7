using P7CreateRestApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Tests.Unit.Controllers
{
    public class TradeControllerTests
    {
        [Fact]
        public async Task GetAll_TradeExiste_RetournerOk()
        {
            // Arrange
            var mockService = new Mock<ITradeService>();
            var mockLogger = new Mock<ILogger<TradeController>>();

            mockService.Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<TradeReadDTO>());

            var controller = new TradeController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.GetAll();

            // Assert
            // Vérifier l’appel au service
            mockService.Verify(s => s.GetAllAsync(), Times.Once);

            // Vérifier le type de retour
            resultat.Should().BeOfType<OkObjectResult>();

            // Vérifier le contenu
            var ok = resultat as OkObjectResult;
            ok.Value.Should().BeAssignableTo<IEnumerable<TradeReadDTO>>();
        }

        [Fact]
        public async Task GetAll_ExceptionLevee_Retourner500()
        {
            // Arrange
            var mockService = new Mock<ITradeService>();
            var mockLogger = new Mock<ILogger<TradeController>>();

            // Simuler une exception dans le service
            mockService.Setup(s => s.GetAllAsync())
                .ThrowsAsync(new Exception("Erreur simulée"));

            var controller = new TradeController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.GetAll();

            // Assert
            mockService.Verify(s => s.GetAllAsync(), Times.Once);

            resultat.Should().BeOfType<ObjectResult>();

            var erreur = resultat as ObjectResult;
            erreur.StatusCode.Should().Be(500);
            erreur.Value.Should().Be("Une erreur interne est survenue.");
        }

        [Fact]
        public async Task GetById_TradeExiste_RetournerOk()
        {
            // Arrange
            var mockService = new Mock<ITradeService>();
            var mockLogger = new Mock<ILogger<TradeController>>();

            mockService.Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync(new TradeReadDTO());

            var controller = new TradeController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Get(1);

            // Assert
            mockService.Verify(s => s.GetByIdAsync(1), Times.Once);

            resultat.Should().BeOfType<OkObjectResult>();

            var ok = resultat as OkObjectResult;
            ok.Value.Should().BeEquivalentTo(new TradeReadDTO());
        }

        [Fact]
        public async Task GetById_TradeInexistant_RetournerNotFound()
        {
            // Arrange
            var mockService = new Mock<ITradeService>();
            var mockLogger = new Mock<ILogger<TradeController>>();

            mockService.Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync((TradeReadDTO?)null);

            var controller = new TradeController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Get(1);

            // Assert
            mockService.Verify(s => s.GetByIdAsync(1), Times.Once);

            resultat.Should().BeOfType<NotFoundObjectResult>();

            var erreur = resultat as NotFoundObjectResult;
            erreur.Value.Should().Be("L'Id renseigné ne correspond à aucun Trade.");
        }

        [Fact]
        public async Task GetById_ExceptionLevee_Retourner500()
        {
            // Arrange
            var mockService = new Mock<ITradeService>();
            var mockLogger = new Mock<ILogger<TradeController>>();

            mockService.Setup(s => s.GetByIdAsync(1))
                .ThrowsAsync(new Exception("Erreur simulée"));

            var controller = new TradeController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Get(1);

            // Assert
            mockService.Verify(s => s.GetByIdAsync(1), Times.Once);

            resultat.Should().BeOfType<ObjectResult>();

            var erreur = resultat as ObjectResult;
            erreur.StatusCode.Should().Be(500);
            erreur.Value.Should().Be("Une erreur interne est survenue.");
        }

        [Fact]
        public async Task Post_TradeExiste_RetournerOk()
        {
            // Arrange
            var mockService = new Mock<ITradeService>();
            var mockLogger = new Mock<ILogger<TradeController>>();

            var dto = new TradeReadDTO { TradeId = 1 };

            mockService.Setup(s => s.CreateAsync(It.IsAny<TradeCreateDTO>()))
                .ReturnsAsync(dto);

            var controller = new TradeController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Create(It.IsAny<TradeCreateDTO>());

            // Assert
            mockService.Verify(s => s.CreateAsync(It.IsAny<TradeCreateDTO>()), Times.Once);

            resultat.Should().BeOfType<CreatedAtActionResult>();

            var ok = resultat as CreatedAtActionResult;
            ok.Value.Should().Be(dto);
            ok.RouteValues["id"].Should().Be(1);
        }

        [Fact]
        public async Task Post_ModelStateInvalide_RetournerBadRequest()
        {
            // Arrange
            var mockService = new Mock<ITradeService>();
            var mockLogger = new Mock<ILogger<TradeController>>();

            var controller = new TradeController(mockService.Object, mockLogger.Object);

            // Rendre ModelState invalide
            controller.ModelState.AddModelError("Test", "Erreur de validation");

            // Act
            var resultat = await controller.Create(It.IsAny<TradeCreateDTO>());

            // Assert
            mockService.Verify(s => s.CreateAsync(It.IsAny<TradeCreateDTO>()), Times.Never);

            resultat.Should().BeOfType<BadRequestObjectResult>();

            var erreur = resultat as BadRequestObjectResult;
            erreur.Value.Should().Be("Les informations fournies pour le Trade sont invalides.");
        }

        [Fact]
        public async Task Post_ExceptionLevee_Retourner500()
        {
            // Arrange
            var mockService = new Mock<ITradeService>();
            var mockLogger = new Mock<ILogger<TradeController>>();

            mockService.Setup(s => s.CreateAsync(It.IsAny<TradeCreateDTO>()))
                .ThrowsAsync(new Exception("Erreur simulée"));

            var controller = new TradeController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Create(It.IsAny<TradeCreateDTO>());

            // Assert
            mockService.Verify(s => s.CreateAsync(It.IsAny<TradeCreateDTO>()), Times.Once);

            resultat.Should().BeOfType<ObjectResult>();

            var erreur = resultat as ObjectResult;
            erreur.StatusCode.Should().Be(500);
            erreur.Value.Should().Be("Une erreur interne est survenue.");
        }

        [Fact]
        public async Task Put_TradeExiste_RetournerOk()
        {
            // Arrange
            var mockService = new Mock<ITradeService>();
            var mockLogger = new Mock<ILogger<TradeController>>();

            mockService.Setup(s => s.UpdateAsync(1, It.IsAny<TradeUpdateDTO>()))
                .ReturnsAsync(new TradeReadDTO());

            var controller = new TradeController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Update(1, It.IsAny<TradeUpdateDTO>());

            // Assert
            mockService.Verify(s => s.UpdateAsync(1, It.IsAny<TradeUpdateDTO>()), Times.Once);

            resultat.Should().BeOfType<OkObjectResult>();

            var ok = resultat as OkObjectResult;
            ok.Value.Should().BeEquivalentTo(new TradeReadDTO());
        }

        [Fact]
        public async Task Put_ModelStateInvalide_RetournerBadRequest()
        {
            // Arrange
            var mockService = new Mock<ITradeService>();
            var mockLogger = new Mock<ILogger<TradeController>>();

            var controller = new TradeController(mockService.Object, mockLogger.Object);

            controller.ModelState.AddModelError("Test", "Erreur de validation");

            // Act
            var resultat = await controller.Update(1, It.IsAny<TradeUpdateDTO>());

            // Assert
            mockService.Verify(s => s.UpdateAsync(1, It.IsAny<TradeUpdateDTO>()), Times.Never);

            resultat.Should().BeOfType<BadRequestObjectResult>();

            var erreur = resultat as BadRequestObjectResult;
            erreur.Value.Should().Be("Les informations fournies pour le Trade sont invalides.");
        }

        [Fact]
        public async Task Put_TradeInexistant_RetournerNotFound()
        {
            // Arrange
            var mockService = new Mock<ITradeService>();
            var mockLogger = new Mock<ILogger<TradeController>>();

            mockService.Setup(s => s.UpdateAsync(1, It.IsAny<TradeUpdateDTO>()))
                .ReturnsAsync((TradeReadDTO?)null);

            var controller = new TradeController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Update(1, It.IsAny<TradeUpdateDTO>());

            // Assert
            mockService.Verify(s => s.UpdateAsync(1, It.IsAny<TradeUpdateDTO>()), Times.Once);

            resultat.Should().BeOfType<NotFoundObjectResult>();

            var erreur = resultat as NotFoundObjectResult;
            erreur.Value.Should().Be("L'Id renseigné ne correspond à aucun Trade.");
        }

        [Fact]
        public async Task Put_ExceptionLevee_Retourner500()
        {
            // Arrange
            var mockService = new Mock<ITradeService>();
            var mockLogger = new Mock<ILogger<TradeController>>();

            mockService.Setup(s => s.UpdateAsync(1, It.IsAny<TradeUpdateDTO>()))
                .ThrowsAsync(new Exception("Erreur simulée"));

            var controller = new TradeController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Update(1, It.IsAny<TradeUpdateDTO>());

            // Assert
            mockService.Verify(s => s.UpdateAsync(1, It.IsAny<TradeUpdateDTO>()), Times.Once);

            resultat.Should().BeOfType<ObjectResult>();

            var erreur = resultat as ObjectResult;
            erreur.StatusCode.Should().Be(500);
            erreur.Value.Should().Be("Une erreur interne est survenue.");
        }

        [Fact]
        public async Task Delete_TradeExiste_RetournerOk()
        {
            // Arrange
            var mockService = new Mock<ITradeService>();
            var mockLogger = new Mock<ILogger<TradeController>>();

            mockService.Setup(s => s.DeleteAsync(1))
                .ReturnsAsync(true);

            var controller = new TradeController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Delete(1);

            // Assert
            mockService.Verify(s => s.DeleteAsync(1), Times.Once);

            resultat.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Delete_TradeInexistant_RetournerNotFound()
        {
            // Arrange
            var mockService = new Mock<ITradeService>();
            var mockLogger = new Mock<ILogger<TradeController>>();

            mockService.Setup(s => s.DeleteAsync(1))
                .ReturnsAsync(false);

            var controller = new TradeController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Delete(1);

            // Assert
            mockService.Verify(s => s.DeleteAsync(1), Times.Once);

            resultat.Should().BeOfType<NotFoundObjectResult>();

            var erreur = resultat as NotFoundObjectResult;
            erreur.Value.Should().Be("L'Id renseigné ne correspond à aucun Trade.");
        }

        [Fact]
        public async Task Delete_ExceptionLevee_Retourner500()
        {
            // Arrange
            var mockService = new Mock<ITradeService>();
            var mockLogger = new Mock<ILogger<TradeController>>();

            mockService.Setup(s => s.DeleteAsync(1))
                .ThrowsAsync(new Exception("Erreur simulée"));

            var controller = new TradeController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Delete(1);

            // Assert
            mockService.Verify(s => s.DeleteAsync(1), Times.Once);

            resultat.Should().BeOfType<ObjectResult>();

            var erreur = resultat as ObjectResult;
            erreur.StatusCode.Should().Be(500);
            erreur.Value.Should().Be("Une erreur interne est survenue.");
        }
    }
}
