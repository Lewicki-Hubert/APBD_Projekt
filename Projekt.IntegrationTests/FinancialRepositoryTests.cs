using Moq;
using Projekt.Context;
using Projekt.Models.Entities;
using Projekt.Models.Financial.Request;
using Projekt.Repositories;

namespace Projekt.IntegrationTests
{
    public class FinancialRepositoryTests
    {
        private readonly FinancialRepository _repository;
        private readonly Mock<AppDbContext> _contextMock;
        private readonly Mock<IAgreementRepository> _agreementRepositoryMock;

        public FinancialRepositoryTests()
        {
            _contextMock = new Mock<AppDbContext>();
            _agreementRepositoryMock = new Mock<IAgreementRepository>();
            _repository = new FinancialRepository(_contextMock.Object, _agreementRepositoryMock.Object);
        }

        [Fact]
        public async Task AddProductAgreementPayment_ShouldAddPayment()
        {
            var request = new ProductAgreementPaymentRequest
            {
                AgreementId = 1,
                Amount = 100,
                PaymentDate = DateTime.UtcNow
            };

            var agreement = new SoftwareContract
            {
                ContractId = 1,
                StartDate = DateTime.UtcNow.AddMonths(-1),
                EndDate = DateTime.UtcNow.AddMonths(1),
                TotalCost = 100
            };

            _agreementRepositoryMock
                .Setup(repo => repo.GetProductAgreementById(1, CancellationToken.None))
                .ReturnsAsync(agreement);
            
            var paymentId = await _repository.AddProductAgreementPayment(request, CancellationToken.None);
            
            Assert.Equal(1, paymentId);
        }
    }
}