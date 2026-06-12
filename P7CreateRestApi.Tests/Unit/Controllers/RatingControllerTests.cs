using Dot.Net.WebApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Tests.Unit.Controllers
{
    public class RatingControllerTests
    {
        [Fact]
        public async Task GetAll_RatingExiste_RetournerOk()
        {
            // Arrange
            var mockService = new Mock<IRatingService>();
            var mockLogger = new Mock<ILogger<RatingController>>();

            mockService.Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<RatingReadDTO>());

            var controller = new RatingController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.GetAll();

            // Assert
            // Vérifier l’appel au service
            mockService.Verify(s => s.GetAllAsync(), Times.Once);

            // Vérifier le type de retour
            resultat.Should().BeOfType<OkObjectResult>();

            // Vérifier le contenu
            var ok = resultat as OkObjectResult;
            ok.Value.Should().BeAssignableTo<IEnumerable<RatingReadDTO>>();
        }

        [Fact]
        public async Task GetAll_ExceptionLevee_Retourner500()
        {
            // Arrange
            var mockService = new Mock<IRatingService>();
            var mockLogger = new Mock<ILogger<RatingController>>();

            // Simuler une exception dans le service
            mockService.Setup(s => s.GetAllAsync())
                .ThrowsAsync(new Exception("Erreur simulée"));

            var controller = new RatingController(mockService.Object, mockLogger.Object);

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
        public async Task GetById_RatingExiste_RetournerOk()
        {
            // Arrange
            var mockService = new Mock<IRatingService>();
            var mockLogger = new Mock<ILogger<RatingController>>();

            mockService.Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync(new RatingReadDTO());

            var controller = new RatingController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Get(1);

            // Assert
            mockService.Verify(s => s.GetByIdAsync(1), Times.Once);

            resultat.Should().BeOfType<OkObjectResult>();

            var ok = resultat as OkObjectResult;
            ok.Value.Should().BeEquivalentTo(new RatingReadDTO());
        }

        [Fact]
        public async Task GetById_RatingInexistant_RetournerNotFound()
        {
            // Arrange
            var mockService = new Mock<IRatingService>();
            var mockLogger = new Mock<ILogger<RatingController>>();

            mockService.Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync((RatingReadDTO?)null);

            var controller = new RatingController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Get(1);

            // Assert
            mockService.Verify(s => s.GetByIdAsync(1), Times.Once);

            resultat.Should().BeOfType<NotFoundObjectResult>();

            var erreur = resultat as NotFoundObjectResult;
            erreur.Value.Should().Be("L'Id renseigné ne correspond à aucun Rating.");
        }

        [Fact]
        public async Task GetById_ExceptionLevee_Retourner500()
        {
            // Arrange
            var mockService = new Mock<IRatingService>();
            var mockLogger = new Mock<ILogger<RatingController>>();

            mockService.Setup(s => s.GetByIdAsync(1))
                .ThrowsAsync(new Exception("Erreur simulée"));

            var controller = new RatingController(mockService.Object, mockLogger.Object);

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
        public async Task Post_RatingExiste_RetournerOk()
        {
            // Arrange
            var mockService = new Mock<IRatingService>();
            var mockLogger = new Mock<ILogger<RatingController>>();

            var dto = new RatingReadDTO { Id = 1 };

            mockService.Setup(s => s.CreateAsync(It.IsAny<RatingCreateDTO>()))
                .ReturnsAsync(dto);

            var controller = new RatingController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Create(It.IsAny<RatingCreateDTO>());

            // Assert
            mockService.Verify(s => s.CreateAsync(It.IsAny<RatingCreateDTO>()), Times.Once);

            resultat.Should().BeOfType<CreatedAtActionResult>();

            var ok = resultat as CreatedAtActionResult;
            ok.Value.Should().Be(dto);
            ok.RouteValues["id"].Should().Be(1);
        }

        [Fact]
        public async Task Post_ModelStateInvalide_RetournerBadRequest()
        {
            // Arrange
            var mockService = new Mock<IRatingService>();
            var mockLogger = new Mock<ILogger<RatingController>>();

            var controller = new RatingController(mockService.Object, mockLogger.Object);

            // Rendre ModelState invalide
            controller.ModelState.AddModelError("Test", "Erreur de validation");

            // Act
            var resultat = await controller.Create(It.IsAny<RatingCreateDTO>());

            // Assert
            mockService.Verify(s => s.CreateAsync(It.IsAny<RatingCreateDTO>()), Times.Never);

            resultat.Should().BeOfType<BadRequestObjectResult>();

            var erreur = resultat as BadRequestObjectResult;
            erreur.Value.Should().Be("Les informations fournies pour le Rating sont invalides.");
        }

        [Fact]
        public async Task Post_ExceptionLevee_Retourner500()
        {
            // Arrange
            var mockService = new Mock<IRatingService>();
            var mockLogger = new Mock<ILogger<RatingController>>();

            mockService.Setup(s => s.CreateAsync(It.IsAny<RatingCreateDTO>()))
                .ThrowsAsync(new Exception("Erreur simulée"));

            var controller = new RatingController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Create(It.IsAny<RatingCreateDTO>());

            // Assert
            mockService.Verify(s => s.CreateAsync(It.IsAny<RatingCreateDTO>()), Times.Once);

            resultat.Should().BeOfType<ObjectResult>();

            var erreur = resultat as ObjectResult;
            erreur.StatusCode.Should().Be(500);
            erreur.Value.Should().Be("Une erreur interne est survenue.");
        }

        [Fact]
        public async Task Put_RatingExiste_RetournerOk()
        {
            // Arrange
            var mockService = new Mock<IRatingService>();
            var mockLogger = new Mock<ILogger<RatingController>>();

            mockService.Setup(s => s.UpdateAsync(1, It.IsAny<RatingUpdateDTO>()))
                .ReturnsAsync(new RatingReadDTO());

            var controller = new RatingController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Update(1, It.IsAny<RatingUpdateDTO>());

            // Assert
            mockService.Verify(s => s.UpdateAsync(1, It.IsAny<RatingUpdateDTO>()), Times.Once);

            resultat.Should().BeOfType<OkObjectResult>();

            var ok = resultat as OkObjectResult;
            ok.Value.Should().BeEquivalentTo(new RatingReadDTO());
        }

        [Fact]
        public async Task Put_ModelStateInvalide_RetournerBadRequest()
        {
            // Arrange
            var mockService = new Mock<IRatingService>();
            var mockLogger = new Mock<ILogger<RatingController>>();

            var controller = new RatingController(mockService.Object, mockLogger.Object);

            controller.ModelState.AddModelError("Test", "Erreur de validation");

            // Act
            var resultat = await controller.Update(1, It.IsAny<RatingUpdateDTO>());

            // Assert
            mockService.Verify(s => s.UpdateAsync(1, It.IsAny<RatingUpdateDTO>()), Times.Never);

            resultat.Should().BeOfType<BadRequestObjectResult>();

            var erreur = resultat as BadRequestObjectResult;
            erreur.Value.Should().Be("Les informations fournies pour le Rating sont invalides.");
        }

        [Fact]
        public async Task Put_RatingInexistant_RetournerNotFound()
        {
            // Arrange
            var mockService = new Mock<IRatingService>();
            var mockLogger = new Mock<ILogger<RatingController>>();

            mockService.Setup(s => s.UpdateAsync(1, It.IsAny<RatingUpdateDTO>()))
                .ReturnsAsync((RatingReadDTO?)null);

            var controller = new RatingController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Update(1, It.IsAny<RatingUpdateDTO>());

            // Assert
            mockService.Verify(s => s.UpdateAsync(1, It.IsAny<RatingUpdateDTO>()), Times.Once);

            resultat.Should().BeOfType<NotFoundObjectResult>();

            var erreur = resultat as NotFoundObjectResult;
            erreur.Value.Should().Be("L'Id renseigné ne correspond à aucun Rating.");
        }

        [Fact]
        public async Task Put_ExceptionLevee_Retourner500()
        {
            // Arrange
            var mockService = new Mock<IRatingService>();
            var mockLogger = new Mock<ILogger<RatingController>>();

            mockService.Setup(s => s.UpdateAsync(1, It.IsAny<RatingUpdateDTO>()))
                .ThrowsAsync(new Exception("Erreur simulée"));

            var controller = new RatingController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Update(1, It.IsAny<RatingUpdateDTO>());

            // Assert
            mockService.Verify(s => s.UpdateAsync(1, It.IsAny<RatingUpdateDTO>()), Times.Once);

            resultat.Should().BeOfType<ObjectResult>();

            var erreur = resultat as ObjectResult;
            erreur.StatusCode.Should().Be(500);
            erreur.Value.Should().Be("Une erreur interne est survenue.");
        }

        [Fact]
        public async Task Delete_RatingExiste_RetournerOk()
        {
            // Arrange
            var mockService = new Mock<IRatingService>();
            var mockLogger = new Mock<ILogger<RatingController>>();

            mockService.Setup(s => s.DeleteAsync(1))
                .ReturnsAsync(true);

            var controller = new RatingController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Delete(1);

            // Assert
            mockService.Verify(s => s.DeleteAsync(1), Times.Once);

            resultat.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Delete_RatingInexistant_RetournerNotFound()
        {
            // Arrange
            var mockService = new Mock<IRatingService>();
            var mockLogger = new Mock<ILogger<RatingController>>();

            mockService.Setup(s => s.DeleteAsync(1))
                .ReturnsAsync(false);

            var controller = new RatingController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Delete(1);

            // Assert
            mockService.Verify(s => s.DeleteAsync(1), Times.Once);

            resultat.Should().BeOfType<NotFoundObjectResult>();

            var erreur = resultat as NotFoundObjectResult;
            erreur.Value.Should().Be("L'Id renseigné ne correspond à aucun Rating.");
        }

        [Fact]
        public async Task Delete_ExceptionLevee_Retourner500()
        {
            // Arrange
            var mockService = new Mock<IRatingService>();
            var mockLogger = new Mock<ILogger<RatingController>>();

            mockService.Setup(s => s.DeleteAsync(1))
                .ThrowsAsync(new Exception("Erreur simulée"));

            var controller = new RatingController(mockService.Object, mockLogger.Object);

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
