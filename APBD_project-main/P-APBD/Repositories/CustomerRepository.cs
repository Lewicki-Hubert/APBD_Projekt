using Microsoft.EntityFrameworkCore;
using Projekt.Context;
using Projekt.Entities;
using Projekt.Enums;
using Projekt.Errors;
using Projekt.Models.Client.Request;

namespace Projekt.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<int> RegisterCorporateClient(CompanyAddRequest corporateAddRequest, CancellationToken cancellationToken)
        {
            if (await _context.Companies.AnyAsync(c => c.CompanyEmail == corporateAddRequest.Email, cancellationToken))
            {
                throw new DuplicatePurchaseException("A company with the same email already exists.");
            }

            if (corporateAddRequest.KRS.Length < 9)
            {
                throw new InvalidInputException("KRS number is too short.");
            }

            try
            {
                var newCustomer = new Customer()
                {
                    Type = CustomerType.Company,
                    IsDeprecated = false
                };

                await _context.Customers.AddAsync(newCustomer, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var newCorporate = new Company()
                {
                    CustomerId = newCustomer.CustomerId,
                    CompanyName = corporateAddRequest.Name,
                    CompanyAddress = corporateAddRequest.Address,
                    CompanyEmail = corporateAddRequest.Email,
                    CompanyPhoneNumber = corporateAddRequest.PhoneNumber,
                    RegistrationNumber = corporateAddRequest.KRS
                };

                await _context.Companies.AddAsync(newCorporate, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return newCustomer.CustomerId;
            }
            catch (Exception ex)
            {
                throw new OperationFailedException("Failed to register corporate client: " + ex.Message);
            }
        }

        public async Task<int> RegisterIndividualClient(IndividualAddRequest individualAddRequest, CancellationToken cancellationToken)
        {
            var newCustomer = new Customer
            {
                Type = CustomerType.Individual,
                IsDeprecated = false
            };

            await _context.Customers.AddAsync(newCustomer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var newIndividual = new Individual
            {
                CustomerId = newCustomer.CustomerId,
                FirstName = individualAddRequest.Name,
                LastName = individualAddRequest.Surname,
                HomeAddress = individualAddRequest.Address,
                EmailAddress = individualAddRequest.Email,
                MobileNumber = individualAddRequest.PhoneNumber,
                NationalId = individualAddRequest.PESEL
            };

            await _context.Individuals.AddAsync(newIndividual, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return newCustomer.CustomerId;
        }

        public async Task<Customer> GetCustomerById(int customerId, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(e => e.CustomerId == customerId, cancellationToken)
                ?? throw new ResourceNotFoundException("Customer does not exist");

            if (customer.IsDeprecated)
            {
                throw new EmptyResultException("Customer has been deprecated");
            }

            return customer;
        }

        public async Task RemoveCustomer(int customerId, CancellationToken cancellationToken)
        {
            var customer = await GetCustomerById(customerId, cancellationToken);

            if (customer.Type == CustomerType.Individual)
            {
                var individual = await _context.Individuals
                    .FirstOrDefaultAsync(e => e.CustomerId == customerId, cancellationToken);

                if (individual != null)
                {
                    individual.FirstName = null;
                    individual.LastName = null;
                    individual.HomeAddress = null;
                    individual.EmailAddress = null;
                    individual.MobileNumber = null;
                    individual.NationalId = null;
                }
            }

            customer.IsDeprecated = true;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateCorporateClient(int customerId, CompanyModifyRequest corporateModifyRequest, CancellationToken cancellationToken)
        {
            var corporate = await _context.Companies
                .FirstOrDefaultAsync(e => e.CustomerId == customerId, cancellationToken)
                ?? throw new ResourceNotFoundException("Company does not exist");

            corporate.CompanyName = corporateModifyRequest.Name;
            corporate.CompanyAddress = corporateModifyRequest.Address;
            corporate.CompanyEmail = corporateModifyRequest.Email;
            corporate.CompanyPhoneNumber = corporateModifyRequest.PhoneNumber;

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateIndividualClient(int customerId, IndividualModifyRequest individualModifyRequest, CancellationToken cancellationToken)
        {
            var individual = await _context.Individuals
                .FirstOrDefaultAsync(e => e.CustomerId == customerId, cancellationToken)
                ?? throw new ResourceNotFoundException("Individual does not exist");

            individual.FirstName = individualModifyRequest.Name;
            individual.LastName = individualModifyRequest.Surname;
            individual.HomeAddress = individualModifyRequest.Address;
            individual.EmailAddress = individualModifyRequest.Email;
            individual.MobileNumber = individualModifyRequest.PhoneNumber;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
