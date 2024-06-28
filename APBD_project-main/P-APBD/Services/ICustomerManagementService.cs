using Projekt.Models.Customer.Request;

namespace Projekt.Services
{
    public interface ICustomerManagementService
    {
        Task<int> RegisterIndividualClient(IndividualAddRequest individualAddRequest, CancellationToken cancellationToken);
        Task<int> RegisterCorporateClient(CompanyAddRequest corporateAddRequest, CancellationToken cancellationToken);
        Task RemoveCustomer(int customerId, CancellationToken cancellationToken);
        Task UpdateIndividualClient(int customerId, IndividualModifyRequest individualModifyRequest, CancellationToken cancellationToken);
        Task UpdateCorporateClient(int customerId, CompanyModifyRequest corporateModifyRequest, CancellationToken cancellationToken);
    }
}