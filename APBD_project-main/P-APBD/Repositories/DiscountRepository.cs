using Microsoft.EntityFrameworkCore;
using Projekt.Context;
using Projekt.Exceptions;
using Projekt.Models.Entities;

namespace Projekt.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly AppDbContext _context;

        public DiscountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Promotion>> GetAllDiscountsAsync(CancellationToken cancellationToken)
        {
            return await _context.Promotions.ToListAsync(cancellationToken);
        }

        public async Task<Promotion> GetDiscountByIdAsync(int discountId, CancellationToken cancellationToken)
        {
            return await _context.Promotions
                .FirstOrDefaultAsync(d => d.PromotionId == discountId, cancellationToken)
                ?? throw new ResourceNotFoundException("Discount not found.");
        }

        public async Task<int> AddDiscountAsync(Promotion discount, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Promotions.AddAsync(discount, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return discount.PromotionId;
            }
            catch (Exception ex)
            {
                throw new OperationFailedException("Failed to save discount to the database: " + ex.Message);
            }
        }

        public async Task UpdateDiscountAsync(Promotion discount, CancellationToken cancellationToken)
        {
            _context.Promotions.Update(discount);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteDiscountAsync(int discountId, CancellationToken cancellationToken)
        {
            var discount = await GetDiscountByIdAsync(discountId, cancellationToken);
            _context.Promotions.Remove(discount);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
