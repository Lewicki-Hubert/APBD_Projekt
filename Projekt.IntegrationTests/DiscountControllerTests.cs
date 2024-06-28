using Microsoft.AspNetCore.Mvc;
using Moq;
using Projekt.Controllers;
using Projekt.Models.Entities;
using Projekt.Services;

namespace Projekt.IntegrationTests
{
    public class DiscountControllerTests
    {
        private readonly DiscountController _controller;
        private readonly Mock<IDiscountService> _serviceMock;

        public DiscountControllerTests()
        {
            _serviceMock = new Mock<IDiscountService>();
            _controller = new DiscountController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAllDiscounts_ShouldReturnDiscounts()
        {
            var result = await _controller.GetAllDiscounts(CancellationToken.None);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddDiscount_ShouldReturnDiscountId()
        {
            var discount = new Promotion
            {
                PromotionName = "New Promotion",
                DiscountValue = 20,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(10)
            };

            _serviceMock
                .Setup(service => service.AddDiscountAsync(discount, CancellationToken.None))
                .ReturnsAsync(1);
            
            var result = await _controller.AddDiscount(discount, CancellationToken.None);
            
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(1, createdAtActionResult.Value);
        }
    }
}