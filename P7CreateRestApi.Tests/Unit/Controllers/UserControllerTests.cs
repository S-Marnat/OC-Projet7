using P7CreateRestApi.Controllers;
using P7CreateRestApi.Domain;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services.Interfaces;
using System.Security.Claims;

namespace P7CreateRestApi.Tests.Unit.Controllers
{
    public class UserControllerTests
    {
        [Fact]
        public async Task GetAll_UserExiste_RetournerOk()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var mockUserManager = MockUserManager();
            var mockLogger = new Mock<ILogger<UserController>>();

            mockService.Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<UserReadDTO>());

            var controller = new UserController(mockService.Object, mockUserManager.Object, mockLogger.Object);

            // Act
            var resultat = await controller.GetAll();

            // Assert
            // Vérifier l’appel au service
            mockService.Verify(s => s.GetAllAsync(), Times.Once);

            // Vérifier le type de retour
            resultat.Should().BeOfType<OkObjectResult>();

            // Vérifier le contenu
            var ok = resultat as OkObjectResult;
            ok.Value.Should().BeAssignableTo<IEnumerable<UserReadDTO>>();
        }

        [Fact]
        public async Task GetAll_ExceptionLevee_Retourner500()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var mockUserManager = MockUserManager();
            var mockLogger = new Mock<ILogger<UserController>>();

            // Simuler une exception dans le service
            mockService.Setup(s => s.GetAllAsync())
                .ThrowsAsync(new Exception("Erreur simulée"));

            var controller = new UserController(mockService.Object, mockUserManager.Object, mockLogger.Object);

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
        public async Task GetById_UserExiste_RetournerOk()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var mockUserManager = MockUserManager();
            var mockLogger = new Mock<ILogger<UserController>>();

            mockService.Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync(new UserReadDTO());

            var controller = new UserController(mockService.Object, mockUserManager.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Get(1);

            // Assert
            mockService.Verify(s => s.GetByIdAsync(1), Times.Once);

            resultat.Should().BeOfType<OkObjectResult>();

            var ok = resultat as OkObjectResult;
            ok.Value.Should().BeEquivalentTo(new UserReadDTO());
        }

        [Fact]
        public async Task GetById_UserInexistant_RetournerNotFound()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var mockUserManager = MockUserManager();
            var mockLogger = new Mock<ILogger<UserController>>();

            mockService.Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync((UserReadDTO?)null);

            var controller = new UserController(mockService.Object, mockUserManager.Object, mockLogger.Object);

            // Act
            var resultat = await controller.Get(1);

            // Assert
            mockService.Verify(s => s.GetByIdAsync(1), Times.Once);

            resultat.Should().BeOfType<NotFoundObjectResult>();

            var erreur = resultat as NotFoundObjectResult;
            erreur.Value.Should().Be("L'Id renseigné ne correspond à aucun User.");
        }

        [Fact]
        public async Task GetById_ExceptionLevee_Retourner500()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var mockUserManager = MockUserManager();
            var mockLogger = new Mock<ILogger<UserController>>();

            mockService.Setup(s => s.GetByIdAsync(1))
                .ThrowsAsync(new Exception("Erreur simulée"));

            var controller = new UserController(mockService.Object, mockUserManager.Object, mockLogger.Object);

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
        public async Task Put_UserExiste_RetournerOk()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var mockUserManager = MockUserManager();
            var mockLogger = new Mock<ILogger<UserController>>();

            var user = new User
            {
                Id = 1,
                UserName = "AncienNom",
                Fullname = "Ancien Nom"
            };

            var dto = new UserUpdateDTO
            {
                UserName = "NouveauNom",
                Fullname = "Nouveau Nom"
            };

            mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(user);

            mockUserManager.Setup(um => um.FindByNameAsync(dto.UserName))
                .ReturnsAsync((User?)null);

            mockUserManager.Setup(um => um.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Success);

            var controller = new UserController(mockService.Object, mockUserManager.Object, mockLogger.Object);

            // Simuler un HttpContext vide
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var resultat = await controller.Update(dto);

            // Assert
            mockUserManager.Verify(um => um.UpdateAsync(It.IsAny<User>()), Times.Once);

            user.UserName.Should().Be("NouveauNom");
            user.NormalizedUserName.Should().Be("NOUVEAUNOM");
            user.Fullname.Should().Be("Nouveau Nom");

            resultat.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Put_AuthentificationInvalide_RetournerUnauthorized()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var mockUserManager = MockUserManager();
            var mockLogger = new Mock<ILogger<UserController>>();

            // Simuler un utilisateur non authentifié
            mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync((User?)null);

            var controller = new UserController(mockService.Object, mockUserManager.Object, mockLogger.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var resultat = await controller.Update(It.IsAny<UserUpdateDTO>());

            // Assert
            mockService.Verify(s => s.UpdateAsync(1, It.IsAny<UserUpdateDTO>()), Times.Never);

            resultat.Should().BeOfType<UnauthorizedResult>();
        }

        [Fact]
        public async Task Put_UserNameExisteDeja_RetournerBadRequest()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var mockUserManager = MockUserManager();
            var mockLogger = new Mock<ILogger<UserController>>();

            var dto = new UserUpdateDTO
            {
                UserName = "NouveauNom"
            };

            // Simuler un UserName existant
            mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(new User { UserName = "AncienNom" });

            mockUserManager.Setup(um => um.FindByNameAsync(dto.UserName))
                .ReturnsAsync(new User());

            var controller = new UserController(mockService.Object, mockUserManager.Object, mockLogger.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var resultat = await controller.Update(dto);

            // Assert
            mockService.Verify(s => s.UpdateAsync(1, It.IsAny<UserUpdateDTO>()), Times.Never);

            resultat.Should().BeOfType<BadRequestObjectResult>();

            var erreur = resultat as BadRequestObjectResult;
            erreur.Value.Should().Be("Nom d'utilisateur déjà utilisé.");
        }

        [Fact]
        public async Task Put_InformationsInvalides_RetournerBadRequest()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var mockUserManager = MockUserManager();
            var mockLogger = new Mock<ILogger<UserController>>();

            // Simuler un utilisateur authentifié
            var fauxUser = new User { Id = 1, UserName = "UserNameTest" };
            mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(fauxUser);

            // Simuler un UpdateAsync qui échoue
            mockUserManager.Setup(um => um.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Failed());

            var controller = new UserController(mockService.Object, mockUserManager.Object, mockLogger.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var resultat = await controller.Update(new UserUpdateDTO());

            // Assert
            mockUserManager.Verify(um => um.UpdateAsync(It.IsAny<User>()), Times.Once);

            resultat.Should().BeOfType<BadRequestObjectResult>();

            var erreur = resultat as BadRequestObjectResult;
            erreur.Value.Should().Be("Les informations de profil fournies sont invalides.");
        }

        [Fact]
        public async Task Put_ExceptionLevee_Retourner500()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var mockUserManager = MockUserManager();
            var mockLogger = new Mock<ILogger<UserController>>();

            var fauxUser = new User { Id = 1, UserName = "UserNameTest" };
            mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(fauxUser);

            mockUserManager.Setup(um => um.UpdateAsync(It.IsAny<User>()))
                .ThrowsAsync(new Exception("Erreur simulée"));

            var controller = new UserController(mockService.Object, mockUserManager.Object, mockLogger.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var resultat = await controller.Update(new UserUpdateDTO());

            // Assert
            mockUserManager.Verify(um => um.UpdateAsync(It.IsAny<User>()), Times.Once);

            resultat.Should().BeOfType<ObjectResult>();

            var erreur = resultat as ObjectResult;
            erreur.StatusCode.Should().Be(500);
            erreur.Value.Should().Be("Une erreur interne est survenue.");
        }

        [Fact]
        public async Task PutPassword_Succes_RetournerOk()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var mockUserManager = MockUserManager();
            var mockLogger = new Mock<ILogger<UserController>>();

            var user = new User();
            var dto = new UserUpdatePasswordDTO
            {
                OldPassword = "AncienMdp",
                NewPassword = "NouveauMdp"
            };

            mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(user);

            mockUserManager.Setup(um => um.CheckPasswordAsync(user, dto.OldPassword))
                .ReturnsAsync(true);

            mockUserManager.Setup(um => um.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword))
                .ReturnsAsync(IdentityResult.Success);

            var controller = new UserController(mockService.Object, mockUserManager.Object, mockLogger.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var resultat = await controller.UpdatePassword(dto);

            // Assert
            mockUserManager.Verify(um => um.ChangePasswordAsync(It.IsAny<User>(), dto.OldPassword, dto.NewPassword), Times.Once);

            resultat.Should().BeOfType<OkObjectResult>();

            var ok = resultat as OkObjectResult;
            ok.Value.Should().Be("Mot de passe modifié avec succès.");
        }

        [Fact]
        public async Task PutPassword_AuthentificationInvalide_RetournerUnauthorized()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var mockUserManager = MockUserManager();
            var mockLogger = new Mock<ILogger<UserController>>();

            mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync((User?)null);

            var controller = new UserController(mockService.Object, mockUserManager.Object, mockLogger.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var resultat = await controller.UpdatePassword(new UserUpdatePasswordDTO());

            // Assert
            mockUserManager.Verify(um => um.ChangePasswordAsync(It.IsAny<User>(), "AncientMdp", "NouveauMdp"), Times.Never);

            resultat.Should().BeOfType<UnauthorizedResult>();
        }

        [Fact]
        public async Task PutPassword_InformationsInvalides_RetournerBadRequest()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var mockUserManager = MockUserManager();
            var mockLogger = new Mock<ILogger<UserController>>();

            var dto = new UserUpdatePasswordDTO
            {
                OldPassword = "AncienMdp",
                NewPassword = "NouveauMdp"
            };

            mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(new User());

            mockUserManager.Setup(um => um.ChangePasswordAsync(It.IsAny<User>(), dto.OldPassword, dto.NewPassword))
                .ReturnsAsync(IdentityResult.Failed());

            var controller = new UserController(mockService.Object, mockUserManager.Object, mockLogger.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var resultat = await controller.UpdatePassword(dto);

            // Assert
            mockUserManager.Verify(um => um.ChangePasswordAsync(It.IsAny<User>(), dto.OldPassword, dto.NewPassword), Times.Once);

            resultat.Should().BeOfType<BadRequestObjectResult>();

            var erreur = resultat as BadRequestObjectResult;
            erreur.Value.Should().Be("Les informations de mot de passe fournies sont invalides.");
        }

        [Fact]
        public async Task PutPassword_ExceptionLevee_Retourner500()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var mockUserManager = MockUserManager();
            var mockLogger = new Mock<ILogger<UserController>>();

            var dto = new UserUpdatePasswordDTO
            {
                OldPassword = "AncienMdp",
                NewPassword = "NouveauMdp"
            };

            mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(new User());

            mockUserManager.Setup(um => um.ChangePasswordAsync(It.IsAny<User>(), dto.OldPassword, dto.NewPassword))
                .ThrowsAsync(new Exception("Erreur simulée"));

            var controller = new UserController(mockService.Object, mockUserManager.Object, mockLogger.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var resultat = await controller.UpdatePassword(dto);

            // Assert
            mockUserManager.Verify(um => um.ChangePasswordAsync(It.IsAny<User>(), dto.OldPassword, dto.NewPassword), Times.Once);

            resultat.Should().BeOfType<ObjectResult>();

            var erreur = resultat as ObjectResult;
            erreur.StatusCode.Should().Be(500);
            erreur.Value.Should().Be("Une erreur interne est survenue.");
        }

        [Fact]
        public async Task Delete_UserExiste_RetournerOk()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var mockUserManager = MockUserManager();
            var mockLogger = new Mock<ILogger<UserController>>();

            mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(new User());

            mockUserManager.Setup(um => um.DeleteAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Success);

            var controller = new UserController(mockService.Object, mockUserManager.Object, mockLogger.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var resultat = await controller.Delete();

            // Assert
            mockUserManager.Verify(um => um.DeleteAsync(It.IsAny<User>()), Times.Once);

            resultat.Should().BeOfType<OkObjectResult>();

            var ok = resultat as OkObjectResult;
            ok.Value.Should().Be("Votre compte a été supprimé avec succès.");
        }

        [Fact]
        public async Task Delete_AuthentificationInvalide_RetournerUnauthorized()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var mockUserManager = MockUserManager();
            var mockLogger = new Mock<ILogger<UserController>>();

            // Simuler un utilisateur non authentifié
            mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync((User?)null);

            mockUserManager.Setup(um => um.DeleteAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Failed());

            var controller = new UserController(mockService.Object, mockUserManager.Object, mockLogger.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var resultat = await controller.Delete();

            // Assert
            mockService.Verify(s => s.GetAllAsync(), Times.Never);
            mockUserManager.Verify(um => um.DeleteAsync(new User()), Times.Never);

            resultat.Should().BeOfType<UnauthorizedResult>();
        }

        [Fact]
        public async Task Delete_InformationsInvalides_RetournerBadRequest()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var mockUserManager = MockUserManager();
            var mockLogger = new Mock<ILogger<UserController>>();

            mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
               .ReturnsAsync(new User());

            mockUserManager.Setup(um => um.DeleteAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Failed());

            var controller = new UserController(mockService.Object, mockUserManager.Object, mockLogger.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var resultat = await controller.Delete();

            // Assert
            mockUserManager.Verify(um => um.DeleteAsync(It.IsAny<User>()), Times.Once);

            resultat.Should().BeOfType<BadRequestObjectResult>();

            var erreur = resultat as BadRequestObjectResult;
            erreur.Value.Should().Be("Impossible de supprimer le compte.");
        }

        [Fact]
        public async Task Delete_ExceptionLevee_Retourner500()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var mockUserManager = MockUserManager();
            var mockLogger = new Mock<ILogger<UserController>>();

            mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
               .ReturnsAsync(new User());

            mockUserManager.Setup(um => um.DeleteAsync(It.IsAny<User>()))
                .ThrowsAsync(new Exception("Erreur simulée"));

            var controller = new UserController(mockService.Object, mockUserManager.Object, mockLogger.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var resultat = await controller.Delete();

            // Assert
            mockUserManager.Verify(um => um.DeleteAsync(It.IsAny<User>()), Times.Once);

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
