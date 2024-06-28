using Projekt.Models.Entities;

namespace Projekt.Services
{
    public interface IDiscountService
    {
        Task<IEnumerable<Promotion>> GetAllDiscountsAsync(CancellationToken cancellationToken);
        Task<Promotion> GetDiscountByIdAsync(int discountId, CancellationToken cancellationToken);
        Task<int> AddDiscountAsync(Promotion discount, CancellationToken cancellationToken);
        Task UpdateDiscountAsync(Promotion discount, CancellationToken cancellationToken);
        Task DeleteDiscountAsync(int discountId, CancellationToken cancellationToken);
    }
}