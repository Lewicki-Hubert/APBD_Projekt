using Moq;
using Projekt.Exceptions;
using Projekt.Models.Agreement;
using Projekt.Repositories;
using Projekt.Services;

namespace Projekt.IntegrationTests
{
    public class AgreementServiceTests
    {
        private readonly AgreementService _service;
        private readonly Mock<IAgreementRepository> _repositoryMock;

        public AgreementServiceTests()
        {
            _repositoryMock = new Mock<IAgreementRepository>();
            _service = new AgreementService(_repositoryMock.Object);
        }

        [Fact]
        public async Task RegisterProductAgreement_ShouldReturnAgreementId()
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

            _repositoryMock
                .Setup(repo => repo.AddProductAgreement(request, 2, CancellationToken.None))
                .ReturnsAsync(1);
            
            var agreementId = await _service.RegisterProductAgreement(request, CancellationToken.None);
            
            Assert.Equal(1, agreementId);
        }

        [Fact]
        public void RegisterProductAgreement_ShouldThrowException_WhenTimespanIsInvalid()
        {
            var request = new ProductAgreementAddRequest
            {
                ClientId = 1,
                ProductId = 1,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1),
                UpdateDescription = "Update",
                SupportExtensionYears = 1
            };
            
            Assert.ThrowsAsync<BusinessRuleViolationException>(() => _service.RegisterProductAgreement(request, CancellationToken.None));
        }
    }
}
