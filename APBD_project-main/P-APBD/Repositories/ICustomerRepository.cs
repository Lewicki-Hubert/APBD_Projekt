using Projekt.Models.Customer.Request;
using Projekt.Models.Entities;

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