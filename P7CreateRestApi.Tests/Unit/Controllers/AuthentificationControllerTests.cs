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

namespace P7CreateRestApi.Tests.Unit.Controllers
{
    public class AuthentificationControllerTests
    {
        [Fact]
        public async Task Register_Succes_RetournerOk()
        {
            // Arrange
            var mockUserManager = MockUserManager();
            var mockSignInManager = MockSignInManager();
            var mockRoleManager = MockRoleManager();
            var mockJwt = MockJwtService();
            var mockLogger = new Mock<ILogger<AuthentificationController>>();

            var dto = new RegisterDTO
            {
                UserName = "UserNameTest",
                Password = "Password123!",
                ConfirmedPassword = "Password123!"
            };

            mockUserManager.Setup(um => um.FindByNameAsync(dto.UserName))
                .ReturnsAsync((User?)null);

            mockUserManager.Setup(um => um.CreateAsync(It.IsAny<User>(), dto.Password))
                .ReturnsAsync(IdentityResult.Success);

            mockUserManager.Setup(um => um.AddToRoleAsync(It.IsAny<User>(), "User"))
                .ReturnsAsync(IdentityResult.Success);

            var controller = new AuthentificationController(
                mockUserManager.Object,
                mockSignInManager.Object,
                mockRoleManager.Object,
                mockJwt.Object,
                mockLogger.Object
            );

            // Simuler un HttpContext vide
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var resultat = await controller.Register(dto);

            // Assert
            mockUserManager.Verify(um => um.CreateAsync(It.IsAny<User>(), dto.Password), Times.Once);

            resultat.Should().BeOfType<OkObjectResult>();

            var ok = resultat as OkObjectResult;
            ok.Value.Should().Be("Utilisateur créé avec succès.");
        }

        [Fact]
        public async Task Register_ModelStateInvalide_RetournerBadRequest()
        {
            // Arrange
            var mockUserManager = MockUserManager();
            var mockSignInManager = MockSignInManager();
            var mockRoleManager = MockRoleManager();
            var mockJwt = MockJwtService();
            var mockLogger = new Mock<ILogger<AuthentificationController>>();

            var controller = new AuthentificationController(
                mockUserManager.Object,
                mockSignInManager.Object,
                mockRoleManager.Object,
                mockJwt.Object,
                mockLogger.Object
            );

            controller.ModelState.AddModelError("Test", "Erreur");

            // Act
            var resultat = await controller.Register(new RegisterDTO());

            // Assert
            mockUserManager.Verify(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);

            resultat.Should().BeOfType<BadRequestObjectResult>();

            var erreur = resultat as BadRequestObjectResult;
            erreur.Value.Should().Be("Les informations fournies pour l'utilisateur sont invalides.");
        }

        [Fact]
        public async Task Register_UserNameExisteDeja_RetournerBadRequest()
        {
            // Arrange
            var mockUserManager = MockUserManager();
            var mockSignInManager = MockSignInManager();
            var mockRoleManager = MockRoleManager();
            var mockJwt = MockJwtService();
            var mockLogger = new Mock<ILogger<AuthentificationController>>();

            var dto = new RegisterDTO
            {
                UserName = "UserNameTest",
                Password = "Password123!",
                ConfirmedPassword = "Password123!"
            };

            // Simuler un UserName existant
            mockUserManager.Setup(um => um.FindByNameAsync(dto.UserName))
                .ReturnsAsync(new User());

            var controller = new AuthentificationController(
                mockUserManager.Object,
                mockSignInManager.Object,
                mockRoleManager.Object,
                mockJwt.Object,
                mockLogger.Object
            );

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var resultat = await controller.Register(dto);

            // Assert
            mockUserManager.Verify(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);

            resultat.Should().BeOfType<BadRequestObjectResult>();

            var erreur = resultat as BadRequestObjectResult;
            erreur.Value.Should().Be("Nom d'utilisateur déjà utilisé.");
        }

        [Fact]
        public async Task Register_MotsDePasseDifferents_RetournerBadRequest()
        {
            // Arrange
            var mockUserManager = MockUserManager();
            var mockSignInManager = MockSignInManager();
            var mockRoleManager = MockRoleManager();
            var mockJwt = MockJwtService();
            var mockLogger = new Mock<ILogger<AuthentificationController>>();

            var dto = new RegisterDTO
            {
                UserName = "UserNameTest",
                Password = "Password123!",
                ConfirmedPassword = "AutrePassword456!"
            };

            var controller = new AuthentificationController(
                mockUserManager.Object,
                mockSignInManager.Object,
                mockRoleManager.Object,
                mockJwt.Object,
                mockLogger.Object
            );

            // Act
            var resultat = await controller.Register(dto);

            // Assert
            mockUserManager.Verify(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);

            resultat.Should().BeOfType<BadRequestObjectResult>();

            var erreur = resultat as BadRequestObjectResult;
            erreur.Value.Should().Be("Les mots de passe ne correspondent pas.");
        }

        [Fact]
        public async Task Register_InformationsInvalides_RetournerBadRequest()
        {
            // Arrange
            var mockUserManager = MockUserManager();
            var mockSignInManager = MockSignInManager();
            var mockRoleManager = MockRoleManager();
            var mockJwt = MockJwtService();
            var mockLogger = new Mock<ILogger<AuthentificationController>>();

            var dto = new RegisterDTO
            {
                UserName = "UserNameTest",
                Password = "Password123!",
                ConfirmedPassword = "Password123!"
            };

            mockUserManager.Setup(um => um.FindByNameAsync(dto.UserName))
                .ReturnsAsync((User?)null);

            mockUserManager.Setup(um => um.CreateAsync(It.IsAny<User>(), dto.Password))
                .ReturnsAsync(IdentityResult.Failed());

            var controller = new AuthentificationController(
                mockUserManager.Object,
                mockSignInManager.Object,
                mockRoleManager.Object,
                mockJwt.Object,
                mockLogger.Object
            );

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var resultat = await controller.Register(dto);

            // Assert
            mockUserManager.Verify(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);

            resultat.Should().BeOfType<BadRequestObjectResult>();

            var erreur = resultat as BadRequestObjectResult;
            erreur.Value.Should().Be("Les informations fournies pour l'utilisateur sont invalides.");
        }

        [Fact]
        public async Task Register_ExceptionLevee_Retourner500()
        {
            // Arrange
            var mockUserManager = MockUserManager();
            var mockSignInManager = MockSignInManager();
            var mockRoleManager = MockRoleManager();
            var mockJwt = MockJwtService();
            var mockLogger = new Mock<ILogger<AuthentificationController>>();

            mockUserManager.Setup(um => um.FindByNameAsync("UserNameTest"))
                .ThrowsAsync(new Exception("Erreur simulée"));

            var controller = new AuthentificationController(
                mockUserManager.Object,
                mockSignInManager.Object,
                mockRoleManager.Object,
                mockJwt.Object,
                mockLogger.Object
            );

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var resultat = await controller.Register(new RegisterDTO());

            // Assert
            mockUserManager.Verify(um => um.FindByNameAsync(It.IsAny<string>()), Times.Once);

            resultat.Should().BeOfType<ObjectResult>();

            var erreur = resultat as ObjectResult;
            erreur.StatusCode.Should().Be(500);
            erreur.Value.Should().Be("Une erreur interne est survenue.");
        }

        [Fact]
        public async Task Login_Succes_RetournerOkAvecToken()
        {
            // Arrange
            var mockUserManager = MockUserManager();
            var mockSignInManager = MockSignInManager();
            var mockRoleManager = MockRoleManager();
            var mockJwt = MockJwtService();
            var mockLogger = new Mock<ILogger<AuthentificationController>>();

            var user = new User();

            var dto = new LoginDTO
            {
                UserName = "UserNameTest",
                Password = "Password123!"
            };

            mockUserManager.Setup(um => um.FindByNameAsync(dto.UserName))
                .ReturnsAsync(user);

            mockSignInManager.Setup(sm => sm.CheckPasswordSignInAsync(user, dto.Password, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            mockJwt.Setup(j => j.GenererTokenAsync(user))
                .ReturnsAsync("FAKE_TOKEN");

            var controller = new AuthentificationController(
                mockUserManager.Object,
                mockSignInManager.Object,
                mockRoleManager.Object,
                mockJwt.Object,
                mockLogger.Object
            );

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var resultat = await controller.Login(dto);

            // Assert
            mockJwt.Verify(um => um.GenererTokenAsync(It.IsAny<User>()), Times.Once);

            resultat.Should().BeOfType<OkObjectResult>();

            var ok = resultat as OkObjectResult;
            ok.Value.Should().BeEquivalentTo(new { Token = "FAKE_TOKEN" });
        }

        [Fact]
        public async Task Login_ModelStateInvalide_RetournerBadRequest()
        {
            // Arrange
            var mockUserManager = MockUserManager();
            var mockSignInManager = MockSignInManager();
            var mockRoleManager = MockRoleManager();
            var mockJwt = MockJwtService();
            var mockLogger = new Mock<ILogger<AuthentificationController>>();

            var controller = new AuthentificationController(
                mockUserManager.Object,
                mockSignInManager.Object,
                mockRoleManager.Object,
                mockJwt.Object,
                mockLogger.Object
            );

            controller.ModelState.AddModelError("Test", "Erreur");

            // Act
            var resultat = await controller.Login(new LoginDTO());

            // Assert
            mockJwt.Verify(um => um.GenererTokenAsync(It.IsAny<User>()), Times.Never);

            resultat.Should().BeOfType<BadRequestObjectResult>();

            var erreur = resultat as BadRequestObjectResult;
            erreur.Value.Should().Be("Les informations fournies pour l'utilisateur sont invalides.");
        }

        [Fact]
        public async Task Login_UserInexistant_RetournerUnauthorized()
        {
            // Arrange
            var mockUserManager = MockUserManager();
            var mockSignInManager = MockSignInManager();
            var mockRoleManager = MockRoleManager();
            var mockJwt = MockJwtService();
            var mockLogger = new Mock<ILogger<AuthentificationController>>();

            var dto = new LoginDTO
            {
                UserName = "UserNameTest",
                Password = "Password123!"
            };

            mockUserManager.Setup(um => um.FindByNameAsync(dto.UserName))
                .ReturnsAsync((User?)null);

            var controller = new AuthentificationController(
                mockUserManager.Object,
                mockSignInManager.Object,
                mockRoleManager.Object,
                mockJwt.Object,
                mockLogger.Object
            );

            // Act
            var resultat = await controller.Login(dto);

            // Assert
            mockJwt.Verify(um => um.GenererTokenAsync(It.IsAny<User>()), Times.Never);

            resultat.Should().BeOfType<UnauthorizedObjectResult>();

            var erreur = resultat as UnauthorizedObjectResult;
            erreur.Value.Should().Be("Identifiants invalides.");
        }

        [Fact]
        public async Task Login_MauvaisMotDePasse_RetournerUnauthorized()
        {
            var mockUserManager = MockUserManager();
            var mockSignInManager = MockSignInManager();
            var mockRoleManager = MockRoleManager();
            var mockJwt = MockJwtService();
            var mockLogger = new Mock<ILogger<AuthentificationController>>();

            var user = new User();

            var dto = new LoginDTO
            {
                UserName = "UserNameTest",
                Password = "Password123!"
            };

            mockUserManager.Setup(um => um.FindByNameAsync(dto.UserName))
                .ReturnsAsync(user);

            mockSignInManager.Setup(sm => sm.CheckPasswordSignInAsync(user, dto.Password, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            var controller = new AuthentificationController(
                mockUserManager.Object,
                mockSignInManager.Object,
                mockRoleManager.Object,
                mockJwt.Object,
                mockLogger.Object
            );

            // Act
            var resultat = await controller.Login(dto);

            // Assert
            mockJwt.Verify(um => um.GenererTokenAsync(It.IsAny<User>()), Times.Never);

            resultat.Should().BeOfType<UnauthorizedObjectResult>();

            var erreur = resultat as UnauthorizedObjectResult;
            erreur.Value.Should().Be("Identifiants invalides.");
        }

        [Fact]
        public async Task Login_Exception_Retourner500()
        {
            //Arrange
            var mockUserManager = MockUserManager();
            var mockSignInManager = MockSignInManager();
            var mockRoleManager = MockRoleManager();
            var mockJwt = MockJwtService();
            var mockLogger = new Mock<ILogger<AuthentificationController>>();

            var user = new User();

            var dto = new LoginDTO
            {
                UserName = "UserNameTest",
                Password = "Password123!"
            };

            mockUserManager.Setup(um => um.FindByNameAsync(dto.UserName))
                .ReturnsAsync(user);

            mockSignInManager.Setup(sm => sm.CheckPasswordSignInAsync(user, dto.Password, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            mockJwt.Setup(j => j.GenererTokenAsync(user))
                .ThrowsAsync(new Exception("Erreur simulée"));

            var controller = new AuthentificationController(
                mockUserManager.Object,
                mockSignInManager.Object,
                mockRoleManager.Object,
                mockJwt.Object,
                mockLogger.Object
            );

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var resultat = await controller.Login(dto);

            // Asseert
            mockUserManager.Verify(um => um.FindByNameAsync(It.IsAny<string>()), Times.Once);

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

        private static Mock<SignInManager<User>> MockSignInManager()
        {
            var userManager = MockUserManager().Object;

            var contextAccessor = new Mock<IHttpContextAccessor>();
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<User>>();

            return new Mock<SignInManager<User>>(
                userManager,
                contextAccessor.Object,
                claimsFactory.Object,
                null, null, null, null
            );
        }

        private static Mock<RoleManager<IdentityRole<int>>> MockRoleManager()
        {
            var store = new Mock<IRoleStore<IdentityRole<int>>>();

            return new Mock<RoleManager<IdentityRole<int>>>(
                store.Object,
                null, null, null, null
            );
        }

        private static Mock<IJwtService> MockJwtService()
        {
            var mock = new Mock<IJwtService>();
            mock.Setup(j => j.GenererTokenAsync(It.IsAny<User>()))
                .ReturnsAsync("FAKE_JWT_TOKEN");
            return mock;
        }
    }
}
