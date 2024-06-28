using Projekt.Models.Contract;

namespace Projekt.Services
{
    public interface IAgreementService
    {
        Task<int> RegisterProductAgreement(ProductAgreementAddRequest productAgreementAddRequest, CancellationToken cancellationToken);
    }
}