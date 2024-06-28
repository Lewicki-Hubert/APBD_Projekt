using Microsoft.EntityFrameworkCore;
using Projekt.Context;
using Projekt.Models.Entities;
using Projekt.Repositories;

namespace Projekt.IntegrationTests
{
    public class DiscountRepositoryTests
    {
        private readonly DiscountRepository _repository;
        private readonly AppDbContext _context;

        public DiscountRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new AppDbContext(options);
            _repository = new DiscountRepository(_context);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            _context.Promotions.Add(new Promotion { PromotionId = 1, PromotionName = "Test Promotion", DiscountValue = 10 });
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllDiscountsAsync_ShouldReturnDiscounts()
        {
            var discounts = await _repository.GetAllDiscountsAsync(CancellationToken.None);
            
            Assert.NotEmpty(discounts);
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
            
            var discountId = await _repository.AddDiscountAsync(discount, CancellationToken.None);
            
            var addedDiscount = await _context.Promotions.FindAsync(discountId);
            Assert.NotNull(addedDiscount);
            Assert.Equal(discount.PromotionName, addedDiscount.PromotionName);
        }
    }
}