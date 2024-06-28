using Projekt.Models.Client.Request;
using Projekt.Repositories;

namespace Projekt.Services
{
    public class CustomerManagementService
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerManagementService(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<int> RegisterIndividualClient(IndividualAddRequest individualAddRequest, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(individualAddRequest.Name) || string.IsNullOrEmpty(individualAddRequest.Surname))
            {
                throw new ArgumentException("Individual name cannot be empty");
            }
            return await _customerRepository.RegisterIndividualClient(individualAddRequest, cancellationToken);
        }

        public async Task<int> RegisterCorporateClient(CompanyAddRequest corporateAddRequest, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(corporateAddRequest.Name))
            {
                throw new ArgumentException("Company name cannot be empty");
            }
            return await _customerRepository.RegisterCorporateClient(corporateAddRequest, cancellationToken);
        }

        public async Task RemoveCustomer(int customerId, CancellationToken cancellationToken)
        {
            var client = await _customerRepository.GetCustomerById(customerId, cancellationToken);
            if (client == null)
            {
                throw new KeyNotFoundException("Customer not found");
            }
            await _customerRepository.RemoveCustomer(customerId, cancellationToken);
        }

        public async Task UpdateIndividualClient(int customerId, IndividualModifyRequest individualModifyRequest, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(individualModifyRequest.Name) || string.IsNullOrEmpty(individualModifyRequest.Surname))
            {
                throw new ArgumentException("Individual name cannot be empty");
            }
            await _customerRepository.UpdateIndividualClient(customerId, individualModifyRequest, cancellationToken);
        }

        public async Task UpdateCorporateClient(int customerId, CompanyModifyRequest corporateModifyRequest, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(corporateModifyRequest.Name))
            {
                throw new ArgumentException("Company name cannot be empty");
            }
            await _customerRepository.UpdateCorporateClient(customerId, corporateModifyRequest, cancellationToken);
        }
    }
}
