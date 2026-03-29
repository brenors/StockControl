using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using StockControl.Application.DTOs.Auth;
using StockControl.Application.Interfaces;
using StockControl.Application.Services;
using StockControl.Domain.Entities;

namespace StockControl.Tests.Services
{

    public class AuthServiceTests
    {
        [Fact]
        public async Task Should_Register_User()
        {
            var repo = new Mock<IUserRepository>();

            var config = new Mock<IConfiguration>();
            config.Setup(x => x["Jwt:Key"]).Returns("super-secret-key-1234567890123456");

            var mapper = new Mock<IMapper>();

            var service = new AuthService(
                repo.Object,
                config.Object,
                mapper.Object
            );

            var request = new RegisterRequest
            {
                Name = "Test",
                Email = "test@test.com",
                Password = "123456",
                Role = "Admin"
            };

            var result = await service.Register(request);

            result.Should().NotBeNull();
            repo.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task Should_Login_With_Valid_Credentials()
        {
            var repo = new Mock<IUserRepository>();

            var user = new User
            {
                Email = "test@test.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")
            };

            repo.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            var config = new Mock<IConfiguration>();
            config.Setup(x => x["Jwt:Key"]).Returns("super-secret-key-1234567890123456");

            var mapper = new Mock<IMapper>();

            var service = new AuthService(
                repo.Object,
                config.Object,
                mapper.Object
            );

            var result = await service.Login(new LoginRequest
            {
                Email = "test@test.com",
                Password = "123456"
            });

            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Should_Fail_Login_With_Invalid_Password()
        {
            var repo = new Mock<IUserRepository>();

            var user = new User
            {
                Email = "test@test.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")
            };

            repo.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            var config = new Mock<IConfiguration>();
            config.Setup(x => x["Jwt:Key"]).Returns("super-secret-key-1234567890123456");

            var mapper = new Mock<IMapper>();

            var service = new AuthService(
                repo.Object,
                config.Object,
                mapper.Object
            );

            Func<Task> act = async () => await service.Login(new LoginRequest
            {
                Email = "test@test.com",
                Password = "wrong"
            });

            await act.Should().ThrowAsync<Exception>();
        }
    }
}
