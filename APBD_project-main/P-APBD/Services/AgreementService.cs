using Projekt.Errors;
using Projekt.Repositories;
using Projekt.Models.Contract;

namespace Projekt.Services
{
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
}