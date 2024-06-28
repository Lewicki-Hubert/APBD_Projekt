using Microsoft.EntityFrameworkCore;
using Projekt.Context;
using Projekt.Exceptions;
using Projekt.Models.Entities;
using Projekt.Models.Financial.Request;
using Projekt.Models.Financial.Responses;

namespace Projekt.Repositories
{
    public class FinancialRepository : IFinancialRepository
    {
        private readonly AppDbContext _context;
        private readonly IAgreementRepository _agreementRepository;

        public FinancialRepository(AppDbContext context, IAgreementRepository agreementRepository)
        {
            _context = context;
            _agreementRepository = agreementRepository;
        }

        public async Task<int> AddProductAgreementPayment(ProductAgreementPaymentRequest request, CancellationToken cancellationToken)
        {
            var productAgreement = await _agreementRepository.GetProductAgreementById(request.AgreementId, cancellationToken);

            ValidatePaymentDate(request.PaymentDate, productAgreement.StartDate, productAgreement.EndDate);
            await EnsurePaymentIsValid(productAgreement, request.Amount, cancellationToken);

            var newPayment = new ContractPayment
            {
                ContractId = request.AgreementId,
                PaymentDescription = request.PaymentDescription,
                PaymentDate = request.PaymentDate,
                Amount = request.Amount
            };

            await _context.ContractPayments.AddAsync(newPayment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return newPayment.PaymentId;
        }

        public async Task<IncomeByProductResponse> CalculateProductForecastIncome(IncomeRequest request, int productId, CancellationToken cancellationToken)
        {
            var product = await _context.Softwares
                .FirstOrDefaultAsync(e => e.SoftwareId == productId, cancellationToken)
                ?? throw new ResourceNotFoundException("Product does not exist");

            decimal totalForecastIncome = 0m;
            var agreements = await _agreementRepository.GetProductAgreements(product.SoftwareId, request.StartDate, request.EndDate, cancellationToken);

            foreach (var agreement in agreements)
            {
                totalForecastIncome += agreement.TotalCost;
            }

            return new IncomeByProductResponse
            {
                ProductName = product.SoftwareName,
                ProductDescription = product.SoftwareDescription,
                TotalIncome = totalForecastIncome
            };
        }

        public async Task<IncomeByProductResponse> CalculateProductActualIncome(IncomeRequest request, int productId, CancellationToken cancellationToken)
        {
            var product = await _context.Softwares
                .FirstOrDefaultAsync(e => e.SoftwareId == productId, cancellationToken)
                ?? throw new ResourceNotFoundException("Product does not exist");

            decimal totalActualIncome = 0m;
            var agreements = await _agreementRepository.GetProductAgreements(product.SoftwareId, request.StartDate, request.EndDate, cancellationToken);

            foreach (var agreement in agreements)
            {
                var totalPayments = await CalculateTotalPayments(agreement, cancellationToken);
                if (totalPayments == agreement.TotalCost)
                {
                    totalActualIncome += agreement.TotalCost;
                }
            }

            return new IncomeByProductResponse
            {
                ProductName = product.SoftwareName,
                ProductDescription = product.SoftwareDescription,
                TotalIncome = totalActualIncome
            };
        }

        public async Task<TotalIncomeSummaryResponse> CalculateTotalForecastIncome(IncomeRequest request, CancellationToken cancellationToken)
        {
            var products = await _context.Softwares.ToListAsync(cancellationToken);
            var productIncomes = new List<IncomeByProductResponse>();
            decimal totalIncome = 0m;

            foreach (var product in products)
            {
                var incomeResponse = await CalculateProductForecastIncome(request, product.SoftwareId, cancellationToken);
                totalIncome += incomeResponse.TotalIncome;
                productIncomes.Add(incomeResponse);
            }

            return new TotalIncomeSummaryResponse
            {
                ProductIncomes = productIncomes,
                OverallIncome = totalIncome
            };
        }

        public async Task<TotalIncomeSummaryResponse> CalculateTotalActualIncome(IncomeRequest request, CancellationToken cancellationToken)
        {
            var products = await _context.Softwares.ToListAsync(cancellationToken);
            var productIncomes = new List<IncomeByProductResponse>();
            decimal totalIncome = 0m;

            foreach (var product in products)
            {
                var incomeResponse = await CalculateProductActualIncome(request, product.SoftwareId, cancellationToken);
                totalIncome += incomeResponse.TotalIncome;
                productIncomes.Add(incomeResponse);
            }

            return new TotalIncomeSummaryResponse
            {
                ProductIncomes = productIncomes,
                OverallIncome = totalIncome
            };
        }

        private async Task<decimal> CalculateTotalPayments(SoftwareContract agreement, CancellationToken cancellationToken)
        {
            return await _context.ContractPayments
                .Where(e => e.ContractId == agreement.ContractId)
                .SumAsync(e => e.Amount, cancellationToken);
        }

        private void ValidatePaymentDate(DateTime paymentDate, DateTime agreementStartDate, DateTime agreementEndDate)
        {
            if (paymentDate < agreementStartDate || paymentDate > agreementEndDate)
            {
                throw new ArgumentException("The PaymentDate is either overdue or too early");
            }
        }

        private async Task EnsurePaymentIsValid(SoftwareContract agreement, decimal paymentValue, CancellationToken cancellationToken)
        {
            var totalPayments = await CalculateTotalPayments(agreement, cancellationToken);

            if (totalPayments >= agreement.TotalCost)
            {
                throw new DuplicatePurchaseException("This agreement has already been fully paid");
            }

            if (totalPayments + paymentValue > agreement.TotalCost)
            {
                throw new ArgumentException("The total payment for this agreement exceeds the total price");
            }
        }
    }
}
