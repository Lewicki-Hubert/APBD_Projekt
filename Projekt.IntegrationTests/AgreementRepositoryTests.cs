using Microsoft.EntityFrameworkCore;
using Projekt.Context;
using Projekt.Models.Agreement;
using Projekt.Models.Entities;
using Projekt.Repositories;

namespace Projekt.IntegrationTests
{

    public class AgreementRepositoryTests
    {
        private readonly AgreementRepository _repository;
        private readonly AppDbContext _context;

        public AgreementRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new AppDbContext(options);
            _repository = new AgreementRepository(new CustomerRepository(_context), _context);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            var customer = new Customer { CustomerId = 1, Type = Models.Base.CustomerType.Company, IsDeprecated = false };
            var software = new Software { SoftwareId = 1, SoftwareName = "Test Software", AnnualCost = 100m };

            _context.Customers.Add(customer);
            _context.Softwares.Add(software);
            _context.SaveChanges();
        }

        [Fact]
        public async Task AddProductAgreement_ShouldAddAgreement()
        {
            // Arrange
            var request = new ProductAgreementAddRequest
            {
                ClientId = 1,
                ProductId = 1,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(10),
                UpdateDescription = "Update",
                SupportExtensionYears = 1
            };

            // Act
            var agreementId = await _repository.AddProductAgreement(request, 2, CancellationToken.None);

            // Assert
            var agreement = await _context.SoftwareContracts.FindAsync(agreementId);
            Assert.NotNull(agreement);
            Assert.Equal(request.ClientId, agreement.CustomerId);
            Assert.Equal(request.ProductId, agreement.SoftwareId);
        }
    }
}
