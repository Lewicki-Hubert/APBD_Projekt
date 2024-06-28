using Microsoft.AspNetCore.Mvc;
using Moq;
using Projekt.Controllers;
using Projekt.Models.Financial.Request;
using Projekt.Services;
using Projekt.Models.Financial.Responses;

namespace Projekt.IntegrationTests
{
    public class FinancialControllerTests
    {
        private readonly FinancialController _controller;
        private readonly Mock<IFinancialService> _serviceMock;

        public FinancialControllerTests()
        {
            _serviceMock = new Mock<IFinancialService>();
            _controller = new FinancialController(_serviceMock.Object);
        }

        [Fact]
        public async Task AddProductAgreementPayment_ShouldReturnOk()
        {
            var request = new ProductAgreementPaymentRequest
            {
                AgreementId = 1,
                Amount = 100,
                PaymentDate = DateTime.UtcNow
            };

            _serviceMock
                .Setup(service => service.RecordProductAgreementPayment(request, CancellationToken.None))
                .ReturnsAsync(1);

            
            var result = await _controller.AddProductAgreementPayment(request, CancellationToken.None);

            
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Successfully added a new product agreement payment, with the id of: 1", okResult.Value);
        }

        [Fact]
        public async Task GetTotalActualIncome_ShouldReturnIncome()
        {
            var request = new IncomeRequest
            {
                StartDate = DateTime.UtcNow.AddMonths(-1),
                EndDate = DateTime.UtcNow
            };

            var response = new TotalIncomeSummaryResponse
            {
                OverallIncome = 1000
            };
            _serviceMock
                .Setup(service => service.CalculateTotalActualIncome(request, null, CancellationToken.None))
                .ReturnsAsync(response);
            
            var result = await _controller.GetTotalActualIncome(request, 0, null, CancellationToken.None);
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value);
        }
    }
}
