using Microsoft.EntityFrameworkCore;
using Projekt.Context;
using Projekt.Exceptions;
using Projekt.Models.Agreement;
using Projekt.Models.Entities;

namespace Projekt.Repositories
{
    public class AgreementRepository : IAgreementRepository
    {
        private readonly AppDbContext _context;
        private readonly ICustomerRepository _customerRepository;

        public AgreementRepository(ICustomerRepository customerRepository, AppDbContext appDbContext)
        {
            _context = appDbContext;
            _customerRepository = customerRepository;
        }

        public async Task<int> AddProductAgreement(ProductAgreementAddRequest request, int supportDuration, CancellationToken cancellationToken)
        {
            await ValidateNoActiveAgreement(request, cancellationToken);

            var customer = await _customerRepository.GetCustomerById(request.ClientId, cancellationToken);
            var product = await _context.Softwares
                .FirstOrDefaultAsync(e => e.SoftwareId == request.ProductId, cancellationToken)
                ?? throw new ResourceNotFoundException("Product does not exist");

            var bestDiscountValue = await GetBestDiscountValue(request, cancellationToken);
            var totalPrice = Math.Round((product.AnnualCost + request.SupportExtensionYears * 1000m) * ((100 - bestDiscountValue) / 100m), 2);

            var newAgreement = new SoftwareContract
            {
                CustomerId = customer.CustomerId,
                SoftwareId = product.SoftwareId,
                SoftwareVersion = product.SoftwareVersion,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                UpdateDescription = request.UpdateDescription,
                SupportDuration = supportDuration,
                PromotionId = (await _context.Promotions
                    .FirstOrDefaultAsync(d => d.StartDate < request.EndDate && d.EndDate > request.StartDate, cancellationToken))?.PromotionId,
                DiscountValue = bestDiscountValue,
                TotalCost = totalPrice
            };

            await _context.SoftwareContracts.AddAsync(newAgreement, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return newAgreement.ContractId;
        }

        private async Task ValidateNoActiveAgreement(ProductAgreementAddRequest request, CancellationToken cancellationToken)
        {
            var existingAgreement = await _context.SoftwareContracts
                .FirstOrDefaultAsync(e => e.CustomerId == request.ClientId && e.SoftwareId == request.ProductId, cancellationToken);

            if (existingAgreement != null &&
                existingAgreement.StartDate < request.EndDate &&
                existingAgreement.EndDate > request.StartDate)
            {
                throw new DuplicatePurchaseException("This client already has an active agreement for this product.");
            }
        }

        private async Task<int> GetBestDiscountValue(ProductAgreementAddRequest request, CancellationToken cancellationToken)
        {
            var bestDiscount = await _context.Promotions
                .Where(d => d.StartDate < request.EndDate && d.EndDate > request.StartDate)
                .OrderByDescending(d => d.DiscountValue)
                .FirstOrDefaultAsync(cancellationToken);

            var discountValue = bestDiscount?.DiscountValue ?? 0;

            if (await _context.SoftwareContracts
                    .AnyAsync(e => e.CustomerId == request.ClientId, cancellationToken) && discountValue < 5)
            {
                discountValue = 5;
            }

            return discountValue;
        }

        public async Task<SoftwareContract> GetProductAgreementById(int agreementId, CancellationToken cancellationToken)
        {
            return await _context.SoftwareContracts
                .FirstOrDefaultAsync(e => e.ContractId == agreementId, cancellationToken)
                ?? throw new ResourceNotFoundException("Product agreement does not exist.");
        }

        public async Task<List<SoftwareContract>> GetProductAgreements(int productId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            return await _context.SoftwareContracts
                .Where(e => e.SoftwareId == productId && e.StartDate < endDate && e.EndDate > startDate)
                .ToListAsync(cancellationToken);
        }
    }
}
