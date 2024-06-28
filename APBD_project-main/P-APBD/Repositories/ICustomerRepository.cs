using Projekt.Entities;
using Projekt.Models.Client.Request;

namespace Projekt.Repositories
{
    public interface ICustomerRepository
    {
        Task<int> RegisterCorporateClient(CompanyAddRequest corporateAddRequest, CancellationToken cancellationToken);
        Task<int> RegisterIndividualClient(IndividualAddRequest individualAddRequest, CancellationToken cancellationToken);
        Task<Customer> GetCustomerById(int customerId, CancellationToken cancellationToken);
        Task RemoveCustomer(int customerId, CancellationToken cancellationToken);
        Task UpdateCorporateClient(int customerId, CompanyModifyRequest corporateModifyRequest, CancellationToken cancellationToken);
        Task UpdateIndividualClient(int customerId, IndividualModifyRequest individualModifyRequest, CancellationToken cancellationToken);
    }
}