using Projekt.Models.Payment.Request;
using Projekt.Repositories;
using Projekt.ApiClients.Interfaces;
using Projekt.Models.Financial.Responses;

namespace Projekt.Services
{
    public class FinancialService : IFinancialService
    {
        private readonly IFinancialRepository _financialRepository;
        private readonly IExchangeRateClient _exchangeRateClient;

        public FinancialService(IFinancialRepository financialRepository, IExchangeRateClient exchangeRateClient)
        {
            _financialRepository = financialRepository;
            _exchangeRateClient = exchangeRateClient;
        }

        public async Task<int> RecordProductAgreementPayment(ProductAgreementPaymentRequest request, CancellationToken cancellationToken)
        {
            request.Validate();
            return await _financialRepository.AddProductAgreementPayment(request, cancellationToken);
        }

        public async Task<IncomeByProductResponse> CalculateProductForecastIncome(IncomeRequest request, int productId, string? currency, CancellationToken cancellationToken)
        {
            request.Validate();
            var productIncome = await _financialRepository.CalculateProductForecastIncome(request, productId, cancellationToken);

            if (!string.IsNullOrEmpty(currency) && currency != "PLN")
            {
                productIncome.TotalIncome = await _exchangeRateClient.ConvertPlnToTargetCurrency(productIncome.TotalIncome, currency, cancellationToken);
            }

            return productIncome;
        }

        public async Task<IncomeByProductResponse> CalculateProductActualIncome(IncomeRequest request, int productId, string? currency, CancellationToken cancellationToken)
        {
            request.Validate();
            var productIncome = await _financialRepository.CalculateProductActualIncome(request, productId, cancellationToken);

            if (!string.IsNullOrEmpty(currency) && currency != "PLN")
            {
                productIncome.TotalIncome = await _exchangeRateClient.ConvertPlnToTargetCurrency(productIncome.TotalIncome, currency, cancellationToken);
            }

            return productIncome;
        }

        public async Task<TotalIncomeSummaryResponse> CalculateTotalForecastIncome(IncomeRequest request, string? currency, CancellationToken cancellationToken)
        {
            request.Validate();
            var totalIncome = await _financialRepository.CalculateTotalForecastIncome(request, cancellationToken);

            if (!string.IsNullOrEmpty(currency) && currency != "PLN")
            {
                totalIncome.CurrencyCode = currency;
                foreach (var product in totalIncome.ProductIncomes)
                {
                    product.TotalIncome = await _exchangeRateClient.ConvertPlnToTargetCurrency(product.TotalIncome, currency, cancellationToken);
                }
                totalIncome.OverallIncome = await _exchangeRateClient.ConvertPlnToTargetCurrency(totalIncome.OverallIncome, currency, cancellationToken);
            }
            else
            {
                totalIncome.CurrencyCode = "PLN";
            }

            return totalIncome;
        }

        public async Task<TotalIncomeSummaryResponse> CalculateTotalActualIncome(IncomeRequest request, string? currency, CancellationToken cancellationToken)
        {
            request.Validate();
            var totalIncome = await _financialRepository.CalculateTotalActualIncome(request, cancellationToken);

            if (!string.IsNullOrEmpty(currency) && currency != "PLN")
            {
                totalIncome.CurrencyCode = currency;
                foreach (var product in totalIncome.ProductIncomes)
                {
                    product.TotalIncome = await _exchangeRateClient.ConvertPlnToTargetCurrency(product.TotalIncome, currency, cancellationToken);
                }
                totalIncome.OverallIncome = await _exchangeRateClient.ConvertPlnToTargetCurrency(totalIncome.OverallIncome, currency, cancellationToken);
            }
            else
            {
                totalIncome.CurrencyCode = "PLN";
            }

            return totalIncome;
        }
    }
}
