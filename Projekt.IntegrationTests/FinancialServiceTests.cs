using Moq;
using Projekt.Models.Financial.Request;
using Projekt.Models.Financial.Responses;
using Projekt.Repositories;
using Projekt.Services;
using Projekt.ApiClients.Interfaces;


namespace Projekt.IntegrationTests
{
    public class FinancialServiceTests
    {
        private readonly FinancialService _service;
        private readonly Mock<IFinancialRepository> _repositoryMock;
        private readonly Mock<IExchangeRateClient> _exchangeRateClientMock;

        public FinancialServiceTests()
        {
            _repositoryMock = new Mock<IFinancialRepository>();
            _exchangeRateClientMock = new Mock<IExchangeRateClient>();
            _service = new FinancialService(_repositoryMock.Object, _exchangeRateClientMock.Object);
        }

        [Fact]
        public async Task RecordProductAgreementPayment_ShouldReturnPaymentId()
        {
            var request = new ProductAgreementPaymentRequest
            {
                AgreementId = 1,
                Amount = 100,
                PaymentDate = DateTime.UtcNow
            };

            _repositoryMock
                .Setup(repo => repo.AddProductAgreementPayment(request, CancellationToken.None))
                .ReturnsAsync(1);
            
            var paymentId = await _service.RecordProductAgreementPayment(request, CancellationToken.None);
            
            Assert.Equal(1, paymentId);
        }

        [Fact]
        public async Task CalculateProductForecastIncome_ShouldReturnIncome()
        {
            var request = new IncomeRequest
            {
                StartDate = DateTime.UtcNow.AddMonths(-1),
                EndDate = DateTime.UtcNow
            };

            var response = new IncomeByProductResponse
            {
                ProductName = "Product",
                ProductDescription = "Description",
                TotalIncome = 1000
            };

            _repositoryMock
                .Setup(repo => repo.CalculateProductForecastIncome(request, 1, CancellationToken.None))
                .ReturnsAsync(response);
            
            var income = await _service.CalculateProductForecastIncome(request, 1, "PLN", CancellationToken.None);
            
            Assert.Equal(response.TotalIncome, income.TotalIncome);
        }
    }
}
