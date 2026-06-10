using Dot.Net.WebApi.Domain;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using P7CreateRestApi.Models;
using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace P7CreateRestApi.Tests.Unit.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task GetAllAsync_AppelerRepository1Fois()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var mockUserManager = MockUserManager();

            mockRepository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<User>());

            var service = new UserService(mockRepository.Object, mockUserManager.Object);

            // Act
            await service.GetAllAsync();

            // Assert
            mockRepository.Verify(r => r.GetAllAsync(), Times.Once);

        }

        [Fact]
        public async Task GetAllAsync_MappingCorrect()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var mockUserManager = MockUserManager();

            var user1 = new User
            {
                Id = 1,
                UserName = "admin",
                Fullname = "Administrateur Système",
            };

            var user2 = new User
            {
                Id = 2,
                UserName = "jdoe",
                Fullname = "John Doe",
            };

            mockRepository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<User> { user1, user2 });

            mockUserManager.Setup(um => um.GetRolesAsync(user1))
                   .ReturnsAsync(new List<string> { "Admin" });

            mockUserManager.Setup(um => um.GetRolesAsync(user2))
                           .ReturnsAsync(new List<string> { "User" });

            var service = new UserService(mockRepository.Object, mockUserManager.Object);

            // Act
            var resultat = await service.GetAllAsync();

            // Assert
            resultat.Should().HaveCount(2);

            resultat.First().Id.Should().Be(1);
            resultat.First().UserName.Should().Be("admin");
            resultat.First().Fullname.Should().Be("Administrateur Système");
            resultat.First().Role.Should().Be("Admin");

            resultat.Last().Id.Should().Be(2);
            resultat.Last().UserName.Should().Be("jdoe");
            resultat.Last().Fullname.Should().Be("John Doe");
            resultat.Last().Role.Should().Be("User");
        }

        [Fact]
        public async Task GetByIdAsync_AppelerRepository1Fois()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var mockUserManager = MockUserManager();

            var user = new User
            {
                Id = 1,
                UserName = "admin",
                Fullname = "Administrateur Système"
            };

            mockRepository.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(user);

            mockUserManager.Setup(um => um.GetRolesAsync(user))
                .ReturnsAsync(new List<string> { "Admin" });

            var service = new UserService(mockRepository.Object, mockUserManager.Object);

            // Act
            await service.GetByIdAsync(1);

            // Assert
            mockRepository.Verify(r => r.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_MappingCorrect()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var mockUserManager = MockUserManager();

            var user = new User
            {
                Id = 1,
                UserName = "admin",
                Fullname = "Administrateur Système"
            };

            mockRepository.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(user);

            mockUserManager.Setup(um => um.GetRolesAsync(user))
                .ReturnsAsync(new List<string> { "Admin" });

            var service = new UserService(mockRepository.Object, mockUserManager.Object);

            // Act
            var resultat = await service.GetByIdAsync(1);

            // Assert
            resultat.Should().NotBeNull();

            resultat.Id.Should().Be(1);
            resultat.UserName.Should().Be("admin");
            resultat.Fullname.Should().Be("Administrateur Système");
            resultat.Role.Should().Be("Admin");
        }

        [Fact]
        public async Task UpdateAsync_AppelerRepository1Fois()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var mockUserManager = MockUserManager();

            var dto = new UserUpdateDTO
            {
                Fullname = "Administrateur Système",
            };

            var userMisAJour = new User
            {
                Id = 1,
                UserName = "admin",
                Fullname = "Administrateur Système"
            };

            mockRepository.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(userMisAJour);

            mockRepository.Setup(r => r.UpdateAsync(1, dto.Fullname))
                .ReturnsAsync(userMisAJour);

            mockUserManager.Setup(um => um.GetRolesAsync(userMisAJour))
                .ReturnsAsync(new List<string> { "Admin" });

            var service = new UserService(mockRepository.Object, mockUserManager.Object);

            // Act
            await service.UpdateAsync(1, dto);

            // Assert
            mockRepository.Verify(r => r.UpdateAsync(1, dto.Fullname), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_MappingCorrect()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var mockUserManager = MockUserManager();

            var dto = new UserUpdateDTO
            {
                Fullname = "Administrateur Système",
            };

            var userMisAJour = new User
            {
                Id = 1,
                UserName = "admin",
                Fullname = "Administrateur Système"
            };

            mockRepository.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(userMisAJour);

            mockRepository.Setup(r => r.UpdateAsync(1, dto.Fullname))
                .ReturnsAsync(userMisAJour);

            mockUserManager.Setup(um => um.GetRolesAsync(userMisAJour))
                .ReturnsAsync(new List<string> { "Admin" });

            var service = new UserService(mockRepository.Object, mockUserManager.Object);

            // Act
            var resultat = await service.UpdateAsync(1, dto);

            // Assert
            resultat.Should().NotBeNull();

            resultat.Id.Should().Be(1);
            resultat.UserName.Should().Be("admin");
            resultat.Fullname.Should().Be("Administrateur Système");
            resultat.Role.Should().Be("Admin");
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
