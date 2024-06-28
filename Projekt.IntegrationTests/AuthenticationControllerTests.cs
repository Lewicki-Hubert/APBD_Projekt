using Microsoft.AspNetCore.Mvc;
using Moq;
using Projekt.Controllers;
using Projekt.Models.Authentication;
using Projekt.Services;


namespace Projekt.IntegrationTests
{
    public class AuthenticationControllerTests
    {
        private readonly AuthenticationController _controller;
        private readonly Mock<IAuthenticationService> _serviceMock;

        public AuthenticationControllerTests()
        {
            _serviceMock = new Mock<IAuthenticationService>();
            _controller = new AuthenticationController(_serviceMock.Object);
        }

        [Fact]
        public async Task SignUp_ShouldReturnOk()
        {
            var request = new SignUpRequest
            {
                Username = "testuser",
                UserPassword = "password",
                IsAdministrator = true
            };

            _serviceMock
                .Setup(service => service.SignUpAsync(request, CancellationToken.None))
                .Returns(Task.CompletedTask);
            
            var result = await _controller.SignUp(request, CancellationToken.None);
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Successfully signed up", okResult.Value);
        }

        [Fact]
        public async Task SignIn_ShouldReturnToken()
        {
            
            var request = new SignInRequest
            {
                Username = "testuser",
                UserPassword = "password"
            };

            _serviceMock
                .Setup(service => service.SignInAsync(request, CancellationToken.None))
                .ReturnsAsync("testtoken");
            
            var result = await _controller.SignIn(request, CancellationToken.None);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(new { Token = "testtoken" }, okResult.Value);
        }
    }
}
