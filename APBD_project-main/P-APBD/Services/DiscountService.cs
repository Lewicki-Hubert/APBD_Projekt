using Projekt.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Projekt.Exceptions;
using Projekt.Models.Entities;

namespace Projekt.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountService(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<IEnumerable<Promotion>> GetAllDiscountsAsync(CancellationToken cancellationToken)
        {
            return await _discountRepository.GetAllDiscountsAsync(cancellationToken);
        }

        public async Task<Promotion> GetDiscountByIdAsync(int discountId, CancellationToken cancellationToken)
        {
            return await _discountRepository.GetDiscountByIdAsync(discountId, cancellationToken);
        }

        public async Task<int> AddDiscountAsync(Promotion discount, CancellationToken cancellationToken)
        {
            if (discount.DiscountValue < 0 || discount.DiscountValue > 100)
            {
                throw new InvalidInputException("Discount value must be between 0 and 100.");
            }

            try
            {
                return await _discountRepository.AddDiscountAsync(discount, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new OperationFailedException("Failed to add discount: " + ex.Message);
            }
        }

        public async Task UpdateDiscountAsync(Promotion discount, CancellationToken cancellationToken)
        {
            await _discountRepository.UpdateDiscountAsync(discount, cancellationToken);
        }

        public async Task DeleteDiscountAsync(int discountId, CancellationToken cancellationToken)
        {
            await _discountRepository.DeleteDiscountAsync(discountId, cancellationToken);
        }
    }
}