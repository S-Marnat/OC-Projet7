
using Dot.Net.WebApi.Controllers;
using Dot.Net.WebApi.Domain;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services.Interfaces;
using System.Security.Claims;

namespace P7CreateRestApi.Tests.Unit.Controllers
{
    public class AdminControllerTests
    {
        [Fact]
        public async Task Put_UserExiste_RetournerOk()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var mockUserManager = MockUserManager();
            var mockLogger = new Mock<ILogger<AdminController>>();

            var user = new User
            {
                Id = 1,
                UserName = "UserNameTest"
            };

            var dto = new UserUpdateRoleDTO
            {
                Role = "Admin"
            };

            // Simuler FindByIdAsync
            mockUserManager.Setup(um => um.FindByIdAsync("1"))
                .ReturnsAsync(user);

            // Simuler GetRolesAsync
            mockUserManager.Setup(um => um.GetRolesAsync(user))
                .ReturnsAsync(new List<string> { "User" });

            // Simuler RemoveFromRolesAsync
            mockUserManager.Setup(um => um.RemoveFromRolesAsync(user, It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(IdentityResult.Success);

            // Simuler AddToRoleAsync
            mockUserManager.Setup(um => um.AddToRoleAsync(user, dto.Role))
                .ReturnsAsync(IdentityResult.Success);

            var controller = new AdminController(mockService.Object, mockUserManager.Object, mockLogger.Object);

            // Simuler un HttpContext vide
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var resultat = await controller.Update(user.Id, dto);

            // Assert
            mockUserManager.Verify(um => um.AddToRoleAsync(It.IsAny<User>(), dto.Role), Times.Once);

            resultat.Should().BeOfType<OkObjectResult>();

            var ok = resultat as OkObjectResult;
            ok.Value.Should().Be("Le rôle de l'utilisateur UserNameTest a été modifié avec succès.");
        }

        [Fact]
        public async Task Put_UserInexistant_RetournerNotFound()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var mockUserManager = MockUserManager();
            var mockLogger = new Mock<ILogger<AdminController>>();

            mockUserManager.Setup(um => um.FindByIdAsync("1"))
                .ReturnsAsync(((User?)null));

            var controller = new AdminController(mockService.Object, mockUserManager.Object, mockLogger.Object);

            // Simuler un HttpContext vide
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var resultat = await controller.Update(1, new UserUpdateRoleDTO());

            // Assert
            mockUserManager.Verify(um => um.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);

            resultat.Should().BeOfType<NotFoundObjectResult>();

            var erreur = resultat as NotFoundObjectResult;
            erreur.Value.Should().Be("L'Id renseigné ne correspond à aucun utilisateur.");
        }

        [Fact]
        public async Task Put_InformationsInvalides_RetournerBadRequest()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var mockUserManager = MockUserManager();
            var mockLogger = new Mock<ILogger<AdminController>>();

            var user = new User
            {
                Id = 1,
                UserName = "UserNameTest"
            };

            var dto = new UserUpdateRoleDTO
            {
                Role = "Admin"
            };

            mockUserManager.Setup(um => um.FindByIdAsync("1"))
                .ReturnsAsync(user);

            mockUserManager.Setup(um => um.GetRolesAsync(user))
                .ReturnsAsync(new List<string> { "User" });

            mockUserManager.Setup(um => um.RemoveFromRolesAsync(user, It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(IdentityResult.Success);

            mockUserManager.Setup(um => um.AddToRoleAsync(user, dto.Role))
                .ReturnsAsync(IdentityResult.Failed());

            var controller = new AdminController(mockService.Object, mockUserManager.Object, mockLogger.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var resultat = await controller.Update(1, dto);

            // Assert
            mockUserManager.Verify(um => um.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);

            resultat.Should().BeOfType<BadRequestObjectResult>();

            var erreur = resultat as BadRequestObjectResult;
            erreur.Value.Should().Be("Le rôle fourni est invalide.");
        }

        [Fact]
        public async Task Put_ExceptionLevee_Retourner500()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var mockUserManager = MockUserManager();
            var mockLogger = new Mock<ILogger<AdminController>>();

            var user = new User
            {
                Id = 1,
                UserName = "UserNameTest"
            };

            var dto = new UserUpdateRoleDTO
            {
                Role = "Admin"
            };

            mockUserManager.Setup(um => um.FindByIdAsync("1"))
                .ReturnsAsync(user);

            mockUserManager.Setup(um => um.GetRolesAsync(user))
                .ReturnsAsync(new List<string> { "User" });

            mockUserManager.Setup(um => um.RemoveFromRolesAsync(user, It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(IdentityResult.Success);

            mockUserManager.Setup(um => um.AddToRoleAsync(user, dto.Role))
                .ThrowsAsync(new Exception("Erreur simulée"));

            var controller = new AdminController(mockService.Object, mockUserManager.Object, mockLogger.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var resultat = await controller.Update(1, dto);

            // Assert
            mockUserManager.Verify(um => um.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);

            resultat.Should().BeOfType<ObjectResult>();

            var erreur = resultat as ObjectResult;
            erreur.StatusCode.Should().Be(500);
            erreur.Value.Should().Be("Une erreur interne est survenue.");
        }


        private static Mock<UserManager<User>> MockUserManager()
        {
            var store = new Mock<IUserStore<User>>();
            return new Mock<UserManager<User>>(
                store.Object, null, null, null, null, null, null, null, null
            );
        }
    }
}
