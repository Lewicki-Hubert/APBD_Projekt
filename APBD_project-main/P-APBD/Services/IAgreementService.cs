using Projekt.Models.Agreement;

namespace Projekt.Services
{
    public interface IAgreementService
    {
        Task<int> RegisterProductAgreement(ProductAgreementAddRequest productAgreementAddRequest, CancellationToken cancellationToken);
    }
}