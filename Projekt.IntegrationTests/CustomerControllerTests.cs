using Microsoft.AspNetCore.Mvc;
using Moq;
using Projekt.Controllers;
using Projekt.Models.Customer.Request;
using Projekt.Services;

namespace Projekt.IntegrationTests
{
    public class CustomerControllerTests
    {
        private readonly CustomerController _controller;
        private readonly Mock<ICustomerManagementService> _serviceMock;

        public CustomerControllerTests()
        {
            _serviceMock = new Mock<ICustomerManagementService>();
            _controller = new CustomerController(_serviceMock.Object);
        }

        [Fact]
        public async Task AddCompanyClient_ShouldReturnOk()
        {
            var request = new CompanyAddRequest
            {
                Name = "Test Company",
                Address = "123 Test St",
                Email = "test@test.com",
                PhoneNumber = "1234567890",
                KRS = "123456789"
            };

            _serviceMock
                .Setup(service => service.RegisterCorporateClient(request, CancellationToken.None))
                .ReturnsAsync(1);
            
            var result = await _controller.AddCompanyClient(request, CancellationToken.None);
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Successfully added new company client, with the id of: 1", okResult.Value);
        }

        [Fact]
        public async Task AddIndividualClient_ShouldReturnOk()
        {
            var request = new IndividualAddRequest
            {
                Name = "John",
                Surname = "Doe",
                Address = "123 Main St",
                Email = "john.doe@test.com",
                PhoneNumber = "123456789",
                PESEL = "12345678901"
            };

            _serviceMock
                .Setup(service => service.RegisterIndividualClient(request, CancellationToken.None))
                .ReturnsAsync(1);
            
            var result = await _controller.AddIndividualClient(request, CancellationToken.None);
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Successfully added new individual client, with the id of: 1", okResult.Value);
        }
    }
}
