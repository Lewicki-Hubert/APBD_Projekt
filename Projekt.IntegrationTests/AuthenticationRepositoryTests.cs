using Microsoft.EntityFrameworkCore;
using Projekt.Context;
using Projekt.Models.Authentication;
using Projekt.Models.Entities;
using Projekt.Repositories;


namespace Projekt.IntegrationTests
{
    public class AuthenticationRepositoryTests
    {
        private readonly AuthenticationRepository _repository;
        private readonly AppDbContext _context;

        public AuthenticationRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new AppDbContext(options);
            _repository = new AuthenticationRepository(_context);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            var user = new ApplicationUser
            {
                UserId = 1,
                Username = "testuser",
                UserPassword = "hashedpassword",
                UserRole = Models.Base.UserRole.Employee
            };

            _context.ApplicationUsers.Add(user);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetUserAsync_ShouldReturnUser()
        {
            var request = new SignInRequest
            {
                Username = "testuser",
                UserPassword = "hashedpassword"
            };
            
            var user = await _repository.GetUserAsync(request, CancellationToken.None);
            Assert.NotNull(user);
            Assert.Equal(request.Username, user.Username);
        }

        [Fact]
        public async Task AddUserAsync_ShouldAddUser()
        {
            var request = new SignUpRequest
            {
                Username = "newuser",
                UserPassword = "newpassword",
                IsAdministrator = false
            };
            
            await _repository.AddUserAsync(request, CancellationToken.None);
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Username == request.Username);
            
            Assert.NotNull(user);
            Assert.Equal(request.Username, user.Username);
        }
    }
}
