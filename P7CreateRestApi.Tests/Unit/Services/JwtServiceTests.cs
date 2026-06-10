using Dot.Net.WebApi.Domain;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using P7CreateRestApi.Services;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

using System.Security.Claims;

namespace P7CreateRestApi.Tests.Unit.Services
{
    public class JwtServiceTests
    {
        [Fact]
        public async Task GenererTokenAsync_AppelerUserManager1Fois()
        {
            // Arrange
            var mockConfiguration = new Mock<IConfiguration>();
            var mockUserManager = MockUserManager();

            var user = new User
            {
                Id = 1,
                UserName = "admin",
                Fullname = "Administrateur Système"
            };

            mockUserManager.Setup(um => um.GetRolesAsync(user))
                           .ReturnsAsync(new List<string> { "Admin" });

            mockConfiguration.Setup(c => c["Jwt:Key"]).Returns("MaCleSuperSecrete123456789");
            mockConfiguration.Setup(c => c["Jwt:Issuer"]).Returns("TestIssuer");
            mockConfiguration.Setup(c => c["Jwt:Audience"]).Returns("TestAudience");
            mockConfiguration.Setup(c => c["Jwt:DurationInMinutes"]).Returns("30");

            var service = new JwtService(mockConfiguration.Object, mockUserManager.Object);

            // Act
            await service.GenererTokenAsync(user);

            // Assert
            mockUserManager.Verify(um => um.GetRolesAsync(user), Times.Once);
        }

        [Fact]
        public async Task GenererTokenAsync_RetourneTokenNonVide()
        {
            // Arrange
            var mockConfiguration = new Mock<IConfiguration>();
            var mockUserManager = MockUserManager();

            var user = new User
            {
                Id = 1,
                UserName = "admin",
                Fullname = "Administrateur Système"
            };

            mockUserManager.Setup(um => um.GetRolesAsync(user))
                           .ReturnsAsync(new List<string> { "Admin" });

            mockConfiguration.Setup(c => c["Jwt:Key"]).Returns("MaCleSuperSecrete123456789");
            mockConfiguration.Setup(c => c["Jwt:Issuer"]).Returns("TestIssuer");
            mockConfiguration.Setup(c => c["Jwt:Audience"]).Returns("TestAudience");
            mockConfiguration.Setup(c => c["Jwt:DurationInMinutes"]).Returns("30");

            var service = new JwtService(mockConfiguration.Object, mockUserManager.Object);

            // Act
            var token = await service.GenererTokenAsync(user);

            // Assert
            token.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GenererTokenAsync_ContientLesBonsClaims()
        {
            // Arrange
            var mockConfiguration = new Mock<IConfiguration>();
            var mockUserManager = MockUserManager();

            var user = new User
            {
                Id = 1,
                UserName = "admin",
                Fullname = "Administrateur Système"
            };

            mockUserManager.Setup(um => um.GetRolesAsync(user))
                           .ReturnsAsync(new List<string> { "Admin" });

            mockConfiguration.Setup(c => c["Jwt:Key"]).Returns("MaCleSuperSecrete123456789");
            mockConfiguration.Setup(c => c["Jwt:Issuer"]).Returns("TestIssuer");
            mockConfiguration.Setup(c => c["Jwt:Audience"]).Returns("TestAudience");
            mockConfiguration.Setup(c => c["Jwt:DurationInMinutes"]).Returns("30");

            var service = new JwtService(mockConfiguration.Object, mockUserManager.Object);

            // Act
            var token = await service.GenererTokenAsync(user);

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            // Assert
            jwt.Claims.Should().Contain(c => c.Type == ClaimTypes.NameIdentifier && c.Value == "1");
            jwt.Claims.Should().Contain(c => c.Type == ClaimTypes.Name && c.Value == "admin");
            jwt.Claims.Should().Contain(c => c.Type == "fullname" && c.Value == "Administrateur Système");
            jwt.Claims.Should().Contain(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
        }

        [Fact]
        public async Task GenererTokenAsync_UtiliseConfiguration()
        {
            // Arrange
            var mockConfiguration = new Mock<IConfiguration>();
            var mockUserManager = MockUserManager();

            var user = new User
            {
                Id = 1,
                UserName = "admin",
                Fullname = "Administrateur Système"
            };

            mockUserManager.Setup(um => um.GetRolesAsync(user))
                           .ReturnsAsync(new List<string>());

            mockConfiguration.Setup(c => c["Jwt:Key"]).Returns("MaCleSuperSecrete123456789");
            mockConfiguration.Setup(c => c["Jwt:Issuer"]).Returns("TestIssuer");
            mockConfiguration.Setup(c => c["Jwt:Audience"]).Returns("TestAudience");
            mockConfiguration.Setup(c => c["Jwt:DurationInMinutes"]).Returns("30");

            var service = new JwtService(mockConfiguration.Object, mockUserManager.Object);

            // Act
            var token = await service.GenererTokenAsync(user);

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            // Assert
            jwt.Issuer.Should().Be("TestIssuer");
            jwt.Audiences.Should().Contain("TestAudience");
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
