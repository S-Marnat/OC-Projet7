using P7CreateRestApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Tests.Unit.Controllers
{
    public class RuleNameControllerTests
    {
        [Fact]
        public async Task GetAll_RuleNameExiste_RetournerOk()
        {
            // Arrange
            var mockService = new Mock<IRuleNameService>();
            var mockLogger = new Mock<ILogger<RuleNameController>>();

            mockService.Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<RuleNameReadDTO>());

            var controller = new RuleNameController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.GetAll();

            // Assert
            // Vérifier l’appel au service
            mockService.Verify(s => s.GetAllAsync(), Times.Once);

            // Vérifier le type de retour
            resultat.Should().BeOfType<OkObjectResult>();

            // Vérifier le contenu
            var ok = resultat as OkObjectResult;
            ok.Value.Should().BeAssignableTo<IEnumerable<RuleNameReadDTO>>();
        }

        [Fact]
        public async Task GetAll_ExceptionLevee_Retourner500()
        {
            // Arrange
            var mockService = new Mock<IRuleNameService>();
            var mockLogger = new Mock<ILogger<RuleNameController>>();

            // Simuler une exception dans le service
            mockService.Setup(s => s.GetAllAsync())
                .ThrowsAsync(new Exception("Erreur simulée"));

            var controller = new RuleNameController(mockService.Object, mockLogger.Object);

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
        public async Task GetById_RuleNameExiste_RetournerOk()
        {
            // Arrange
            var mockService = new Mock<IRuleNameService>();
            var mockLogger = new Mock<ILogger<RuleNameController>>();

            mockService.Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync(new RuleNameReadDTO());

            var controller = new RuleNameController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Get(1);

            // Assert
            mockService.Verify(s => s.GetByIdAsync(1), Times.Once);

            resultat.Should().BeOfType<OkObjectResult>();

            var ok = resultat as OkObjectResult;
            ok.Value.Should().BeEquivalentTo(new RuleNameReadDTO());
        }

        [Fact]
        public async Task GetById_RuleNameInexistant_RetournerNotFound()
        {
            // Arrange
            var mockService = new Mock<IRuleNameService>();
            var mockLogger = new Mock<ILogger<RuleNameController>>();

            mockService.Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync((RuleNameReadDTO?)null);

            var controller = new RuleNameController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Get(1);

            // Assert
            mockService.Verify(s => s.GetByIdAsync(1), Times.Once);

            resultat.Should().BeOfType<NotFoundObjectResult>();

            var erreur = resultat as NotFoundObjectResult;
            erreur.Value.Should().Be("L'Id renseigné ne correspond à aucun RuleName.");
        }

        [Fact]
        public async Task GetById_ExceptionLevee_Retourner500()
        {
            // Arrange
            var mockService = new Mock<IRuleNameService>();
            var mockLogger = new Mock<ILogger<RuleNameController>>();

            mockService.Setup(s => s.GetByIdAsync(1))
                .ThrowsAsync(new Exception("Erreur simulée"));

            var controller = new RuleNameController(mockService.Object, mockLogger.Object);

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
        public async Task Post_RuleNameExiste_RetournerOk()
        {
            // Arrange
            var mockService = new Mock<IRuleNameService>();
            var mockLogger = new Mock<ILogger<RuleNameController>>();

            var dto = new RuleNameReadDTO { Id = 1 };

            mockService.Setup(s => s.CreateAsync(It.IsAny<RuleNameCreateDTO>()))
                .ReturnsAsync(dto);

            var controller = new RuleNameController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Create(It.IsAny<RuleNameCreateDTO>());

            // Assert
            mockService.Verify(s => s.CreateAsync(It.IsAny<RuleNameCreateDTO>()), Times.Once);

            resultat.Should().BeOfType<CreatedAtActionResult>();

            var ok = resultat as CreatedAtActionResult;
            ok.Value.Should().Be(dto);
            ok.RouteValues["id"].Should().Be(1);
        }

        [Fact]
        public async Task Post_ModelStateInvalide_RetournerBadRequest()
        {
            // Arrange
            var mockService = new Mock<IRuleNameService>();
            var mockLogger = new Mock<ILogger<RuleNameController>>();

            var controller = new RuleNameController(mockService.Object, mockLogger.Object);

            // Rendre ModelState invalide
            controller.ModelState.AddModelError("Test", "Erreur de validation");

            // Act
            var resultat = await controller.Create(It.IsAny<RuleNameCreateDTO>());

            // Assert
            mockService.Verify(s => s.CreateAsync(It.IsAny<RuleNameCreateDTO>()), Times.Never);

            resultat.Should().BeOfType<BadRequestObjectResult>();

            var erreur = resultat as BadRequestObjectResult;
            erreur.Value.Should().Be("Les informations fournies pour le RuleName sont invalides.");
        }

        [Fact]
        public async Task Post_ExceptionLevee_Retourner500()
        {
            // Arrange
            var mockService = new Mock<IRuleNameService>();
            var mockLogger = new Mock<ILogger<RuleNameController>>();

            mockService.Setup(s => s.CreateAsync(It.IsAny<RuleNameCreateDTO>()))
                .ThrowsAsync(new Exception("Erreur simulée"));

            var controller = new RuleNameController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Create(It.IsAny<RuleNameCreateDTO>());

            // Assert
            mockService.Verify(s => s.CreateAsync(It.IsAny<RuleNameCreateDTO>()), Times.Once);

            resultat.Should().BeOfType<ObjectResult>();

            var erreur = resultat as ObjectResult;
            erreur.StatusCode.Should().Be(500);
            erreur.Value.Should().Be("Une erreur interne est survenue.");
        }

        [Fact]
        public async Task Put_RuleNameExiste_RetournerOk()
        {
            // Arrange
            var mockService = new Mock<IRuleNameService>();
            var mockLogger = new Mock<ILogger<RuleNameController>>();

            mockService.Setup(s => s.UpdateAsync(1, It.IsAny<RuleNameUpdateDTO>()))
                .ReturnsAsync(new RuleNameReadDTO());

            var controller = new RuleNameController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Update(1, It.IsAny<RuleNameUpdateDTO>());

            // Assert
            mockService.Verify(s => s.UpdateAsync(1, It.IsAny<RuleNameUpdateDTO>()), Times.Once);

            resultat.Should().BeOfType<OkObjectResult>();

            var ok = resultat as OkObjectResult;
            ok.Value.Should().BeEquivalentTo(new RuleNameReadDTO());
        }

        [Fact]
        public async Task Put_ModelStateInvalide_RetournerBadRequest()
        {
            // Arrange
            var mockService = new Mock<IRuleNameService>();
            var mockLogger = new Mock<ILogger<RuleNameController>>();

            var controller = new RuleNameController(mockService.Object, mockLogger.Object);

            controller.ModelState.AddModelError("Test", "Erreur de validation");

            // Act
            var resultat = await controller.Update(1, It.IsAny<RuleNameUpdateDTO>());

            // Assert
            mockService.Verify(s => s.UpdateAsync(1, It.IsAny<RuleNameUpdateDTO>()), Times.Never);

            resultat.Should().BeOfType<BadRequestObjectResult>();

            var erreur = resultat as BadRequestObjectResult;
            erreur.Value.Should().Be("Les informations fournies pour le RuleName sont invalides.");
        }

        [Fact]
        public async Task Put_RuleNameInexistant_RetournerNotFound()
        {
            // Arrange
            var mockService = new Mock<IRuleNameService>();
            var mockLogger = new Mock<ILogger<RuleNameController>>();

            mockService.Setup(s => s.UpdateAsync(1, It.IsAny<RuleNameUpdateDTO>()))
                .ReturnsAsync((RuleNameReadDTO?)null);

            var controller = new RuleNameController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Update(1, It.IsAny<RuleNameUpdateDTO>());

            // Assert
            mockService.Verify(s => s.UpdateAsync(1, It.IsAny<RuleNameUpdateDTO>()), Times.Once);

            resultat.Should().BeOfType<NotFoundObjectResult>();

            var erreur = resultat as NotFoundObjectResult;
            erreur.Value.Should().Be("L'Id renseigné ne correspond à aucun RuleName.");
        }

        [Fact]
        public async Task Put_ExceptionLevee_Retourner500()
        {
            // Arrange
            var mockService = new Mock<IRuleNameService>();
            var mockLogger = new Mock<ILogger<RuleNameController>>();

            mockService.Setup(s => s.UpdateAsync(1, It.IsAny<RuleNameUpdateDTO>()))
                .ThrowsAsync(new Exception("Erreur simulée"));

            var controller = new RuleNameController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Update(1, It.IsAny<RuleNameUpdateDTO>());

            // Assert
            mockService.Verify(s => s.UpdateAsync(1, It.IsAny<RuleNameUpdateDTO>()), Times.Once);

            resultat.Should().BeOfType<ObjectResult>();

            var erreur = resultat as ObjectResult;
            erreur.StatusCode.Should().Be(500);
            erreur.Value.Should().Be("Une erreur interne est survenue.");
        }

        [Fact]
        public async Task Delete_RuleNameExiste_RetournerOk()
        {
            // Arrange
            var mockService = new Mock<IRuleNameService>();
            var mockLogger = new Mock<ILogger<RuleNameController>>();

            mockService.Setup(s => s.DeleteAsync(1))
                .ReturnsAsync(true);

            var controller = new RuleNameController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Delete(1);

            // Assert
            mockService.Verify(s => s.DeleteAsync(1), Times.Once);

            resultat.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Delete_RuleNameInexistant_RetournerNotFound()
        {
            // Arrange
            var mockService = new Mock<IRuleNameService>();
            var mockLogger = new Mock<ILogger<RuleNameController>>();

            mockService.Setup(s => s.DeleteAsync(1))
                .ReturnsAsync(false);

            var controller = new RuleNameController(mockService.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Delete(1);

            // Assert
            mockService.Verify(s => s.DeleteAsync(1), Times.Once);

            resultat.Should().BeOfType<NotFoundObjectResult>();

            var erreur = resultat as NotFoundObjectResult;
            erreur.Value.Should().Be("L'Id renseigné ne correspond à aucun RuleName.");
        }

        [Fact]
        public async Task Delete_ExceptionLevee_Retourner500()
        {
            // Arrange
            var mockService = new Mock<IRuleNameService>();
            var mockLogger = new Mock<ILogger<RuleNameController>>();

            mockService.Setup(s => s.DeleteAsync(1))
                .ThrowsAsync(new Exception("Erreur simulée"));

            var controller = new RuleNameController(mockService.Object, mockLogger.Object);

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
