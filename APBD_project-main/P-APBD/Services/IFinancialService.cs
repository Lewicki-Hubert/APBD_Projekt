using Projekt.Models.Financial.Request;
using Projekt.Models.Financial.Responses;

namespace Projekt.Services
{
    public interface IFinancialService
    {
        Task<int> RecordProductAgreementPayment(ProductAgreementPaymentRequest request, CancellationToken cancellationToken);
        Task<IncomeByProductResponse> CalculateProductForecastIncome(IncomeRequest request, int productId, string? currency, CancellationToken cancellationToken);
        Task<IncomeByProductResponse> CalculateProductActualIncome(IncomeRequest request, int productId, string? currency, CancellationToken cancellationToken);
        Task<TotalIncomeSummaryResponse> CalculateTotalForecastIncome(IncomeRequest request, string? currency, CancellationToken cancellationToken);
        Task<TotalIncomeSummaryResponse> CalculateTotalActualIncome(IncomeRequest request, string? currency, CancellationToken cancellationToken);
    }
}