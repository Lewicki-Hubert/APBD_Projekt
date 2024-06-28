using Projekt.Models.Customer.Request;
using Projekt.Services;
using Projekt.Repositories;
using Moq;

namespace Projekt.IntegrationTests
{
    public class CustomerManagementServiceTests
    {
        private readonly CustomerManagementService _service;
        private readonly Mock<ICustomerRepository> _repositoryMock;

        public CustomerManagementServiceTests()
        {
            _repositoryMock = new Mock<ICustomerRepository>();
            _service = new CustomerManagementService(_repositoryMock.Object);
        }

        [Fact]
        public async Task RegisterIndividualClient_ShouldRegisterClient()
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

            _repositoryMock
                .Setup(repo => repo.RegisterIndividualClient(request, CancellationToken.None))
                .ReturnsAsync(1);
            
            var clientId = await _service.RegisterIndividualClient(request, CancellationToken.None);
            
            Assert.Equal(1, clientId);
        }

        [Fact]
        public async Task RemoveCustomer_ShouldMarkCustomerAsDeprecated()
        {
            var customerId = 1;
            _repositoryMock.Setup(repo => repo.GetCustomerById(customerId, CancellationToken.None))
                .ReturnsAsync(new Projekt.Models.Entities.Customer { CustomerId = customerId });
            
            await _service.RemoveCustomer(customerId, CancellationToken.None);
            _repositoryMock.Verify(repo => repo.RemoveCustomer(customerId, CancellationToken.None), Times.Once);
        }
    }
}