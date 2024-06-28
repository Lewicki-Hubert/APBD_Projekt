using Projekt.Exceptions;
using Projekt.Models.Agreement;
using Projekt.Repositories;
using Projekt.Services;

public class AgreementService : IAgreementService
{
    private readonly IAgreementRepository _agreementRepository;

    public AgreementService(IAgreementRepository agreementRepository)
    {
        _agreementRepository = agreementRepository;
    }

    public async Task<int> RegisterProductAgreement(ProductAgreementAddRequest productAgreementAddRequest, CancellationToken cancellationToken)
    {
        ValidateRequestBody(productAgreementAddRequest);
        EnsureValidTimespan(productAgreementAddRequest.StartDate, productAgreementAddRequest.EndDate);
        EnsureValidSupportExtension(productAgreementAddRequest.SupportExtensionYears);

        var totalSupportDuration = 1 + productAgreementAddRequest.SupportExtensionYears;

        return await _agreementRepository.AddProductAgreement(productAgreementAddRequest, totalSupportDuration, cancellationToken);
    }

    public async Task UpdateProductAgreement(int agreementId, ProductAgreementAddRequest productAgreementAddRequest, CancellationToken cancellationToken)
    {
        var agreement = await _agreementRepository.GetProductAgreementById(agreementId, cancellationToken);
        if (agreement == null)
        {
            throw new ResourceNotFoundException("Agreement not found");
        }

        agreement.StartDate = productAgreementAddRequest.StartDate;
        agreement.EndDate = productAgreementAddRequest.EndDate;
        agreement.UpdateDescription = productAgreementAddRequest.UpdateDescription;
        agreement.SupportDuration = productAgreementAddRequest.SupportExtensionYears + 1;

        await _agreementRepository.UpdateProductAgreement(agreement, cancellationToken);
    }

    public Task<object> IsAgreementActive(int agreementId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private void ValidateRequestBody(ProductAgreementAddRequest request)
    {
        request.Validate();
    }

    private void EnsureValidTimespan(DateTime startDate, DateTime endDate)
    {
        int dayDifference = (endDate - startDate).Days;
        if (dayDifference < 3 || dayDifference > 30)
        {
            throw new BusinessRuleViolationException("The time difference between dates must be between 3 and 30 days");
        }
    }

    private void EnsureValidSupportExtension(int supportExtensionYears)
    {
        if (supportExtensionYears < 0 || supportExtensionYears > 3)
        {
            throw new BusinessRuleViolationException("The SupportExtensionYears must be between 0 and 3 years");
        }
    }
    
}
