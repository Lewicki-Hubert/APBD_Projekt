using Moq;
using Projekt.Models.Entities;
using Projekt.Repositories;
using Projekt.Services;

namespace Projekt.IntegrationTests
{
    public class DiscountServiceTests
    {
        private readonly DiscountService _service;
        private readonly Mock<IDiscountRepository> _repositoryMock;

        public DiscountServiceTests()
        {
            _repositoryMock = new Mock<IDiscountRepository>();
            _service = new DiscountService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetAllDiscountsAsync_ShouldReturnDiscounts()
        {
            var discounts = await _service.GetAllDiscountsAsync(CancellationToken.None);
            Assert.NotNull(discounts);
        }

        [Fact]
        public async Task AddDiscountAsync_ShouldAddDiscount()
        {
            var discount = new Promotion
            {
                PromotionName = "New Promotion",
                DiscountValue = 20,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(10)
            };

            _repositoryMock
                .Setup(repo => repo.AddDiscountAsync(discount, CancellationToken.None))
                .ReturnsAsync(1);
            
            var discountId = await _service.AddDiscountAsync(discount, CancellationToken.None);
            
            Assert.Equal(1, discountId);
        }
    }
}