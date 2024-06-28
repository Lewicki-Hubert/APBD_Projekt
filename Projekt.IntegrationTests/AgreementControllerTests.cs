using Microsoft.AspNetCore.Mvc;
using Moq;
using Projekt.Controllers;
using Projekt.Models.Agreement;
using Projekt.Services;

namespace Projekt.IntegrationTests.Controllers
{
    public class AgreementControllerTests
    {
        private readonly AgreementController _controller;
        private readonly Mock<IAgreementService> _serviceMock;

        public AgreementControllerTests()
        {
            _serviceMock = new Mock<IAgreementService>();
            _controller = new AgreementController(_serviceMock.Object);
        }

        [Fact]
        public async Task AddProductAgreement_ShouldReturnOk()
        {
            var request = new ProductAgreementAddRequest
            {
                ClientId = 1,
                ProductId = 1,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(10),
                UpdateDescription = "Update",
                SupportExtensionYears = 1
            };

            _serviceMock
                .Setup(service => service.RegisterProductAgreement(request, CancellationToken.None))
                .ReturnsAsync(1);
            
            var result = await _controller.AddProductAgreement(request, CancellationToken.None);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Successfully added a new product agreement, with the id of: 1", okResult.Value);
        }
    }
}