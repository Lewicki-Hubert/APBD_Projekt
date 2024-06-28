using Projekt.Models.Agreement;

namespace Projekt.Services
{
    public interface IAgreementService
    {
        Task<int> RegisterProductAgreement(ProductAgreementAddRequest productAgreementAddRequest, CancellationToken cancellationToken);
        Task UpdateProductAgreement(int agreementId, ProductAgreementAddRequest productAgreementAddRequest, CancellationToken cancellationToken);
        Task<object> IsAgreementActive(int agreementId, CancellationToken cancellationToken);
    }
}