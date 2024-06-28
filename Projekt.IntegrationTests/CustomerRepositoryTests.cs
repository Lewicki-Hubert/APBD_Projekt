using Microsoft.EntityFrameworkCore;
using Projekt.Context;
using Projekt.Models.Customer.Request;
using Projekt.Models.Entities;
using Projekt.Repositories;

namespace Projekt.IntegrationTests
{
    public class CustomerRepositoryTests
    {
        private readonly CustomerRepository _repository;
        private readonly AppDbContext _context;

        public CustomerRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new AppDbContext(options);
            _repository = new CustomerRepository(_context);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            _context.Customers.Add(new Customer { CustomerId = 1, Type = Models.Base.CustomerType.Company, IsDeprecated = false });
            _context.SaveChanges();
        }

        [Fact]
        public async Task RegisterCorporateClient_ShouldRegisterClient()
        {
            var request = new CompanyAddRequest
            {
                Name = "Test Company",
                Address = "123 Test St",
                Email = "test@test.com",
                PhoneNumber = "1234567890",
                KRS = "123456789"
            };
            
            var customerId = await _repository.RegisterCorporateClient(request, CancellationToken.None);
            
            var customer = await _context.Companies.FindAsync(customerId);
            Assert.NotNull(customer);
            Assert.Equal(request.Name, customer.CompanyName);
        }

        [Fact]
        public async Task GetCustomerById_ShouldReturnCustomer()
        {
            var customer = await _repository.GetCustomerById(1, CancellationToken.None);
            
            Assert.NotNull(customer);
            Assert.Equal(1, customer.CustomerId);
        }
    }
}
