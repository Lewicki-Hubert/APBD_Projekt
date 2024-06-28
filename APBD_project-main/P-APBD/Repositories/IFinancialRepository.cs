using Projekt.Models.Financial.Request;
using Projekt.Models.Financial.Responses;

namespace Projekt.Repositories
{
    public interface IFinancialRepository
    {
        Task<int> AddProductAgreementPayment(ProductAgreementPaymentRequest request, CancellationToken cancellationToken);
        Task<IncomeByProductResponse> CalculateProductForecastIncome(IncomeRequest request, int productId, CancellationToken cancellationToken);
        Task<IncomeByProductResponse> CalculateProductActualIncome(IncomeRequest request, int productId, CancellationToken cancellationToken);
        Task<TotalIncomeSummaryResponse> CalculateTotalForecastIncome(IncomeRequest request, CancellationToken cancellationToken);
        Task<TotalIncomeSummaryResponse> CalculateTotalActualIncome(IncomeRequest request, CancellationToken cancellationToken);
    }
}