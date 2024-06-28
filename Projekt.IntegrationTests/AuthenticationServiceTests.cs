using Moq;
using Projekt.Models.Authentication;
using Projekt.Models.Entities;
using Projekt.Repositories;
using Projekt.Services;
using Projekt.Utilities;
using Microsoft.Extensions.Configuration;
using Projekt.Models.Base;


namespace Projekt.IntegrationTests
{
    public class AuthenticationServiceTests
    {
        private readonly AuthenticationService _service;
        private readonly Mock<IAuthenticationRepository> _repositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;

        public AuthenticationServiceTests()
        {
            _repositoryMock = new Mock<IAuthenticationRepository>();
            _configurationMock = new Mock<IConfiguration>();
            _service = new AuthenticationService(_repositoryMock.Object, _configurationMock.Object);
        }

        [Fact]
        public async Task SignInAsync_ShouldReturnToken()
        {
            var request = new SignInRequest
            {
                Username = "testuser",
                UserPassword = "password"
            };

            var user = new ApplicationUser
            {
                Username = "testuser",
                UserPassword = PasswordSecurity.HashPassword("password"),
                UserRole = UserRole.Administrator
            };

            _repositoryMock
                .Setup(repo => repo.GetUserAsync(request, CancellationToken.None))
                .ReturnsAsync(user);

            _configurationMock
                .Setup(config => config["Jwt:SecretKey"])
                .Returns("supersecretkey");
            _configurationMock
                .Setup(config => config["Jwt:Issuer"])
                .Returns("testissuer");
            _configurationMock
                .Setup(config => config["Jwt:Audience"])
                .Returns("testaudience");
            
            var token = await _service.SignInAsync(request, CancellationToken.None);
            
            Assert.NotNull(token);
        }

        [Fact]
        public async Task SignUpAsync_ShouldAddUser()
        {
            var request = new SignUpRequest
            {
                Username = "newuser",
                UserPassword = "password",
                IsAdministrator = true
            };
            
            await _service.SignUpAsync(request, CancellationToken.None);
            _repositoryMock.Verify(repo => repo.AddUserAsync(request, CancellationToken.None), Times.Once);
        }
    }
}
