using Microsoft.EntityFrameworkCore;
using Projekt.Context;
using Projekt.Entities;
using Projekt.Enums;
using Projekt.Errors;
using Projekt.Models.Login;
using Projekt.Utilities;

namespace Projekt.Repositories
{
    public class AuthenticationRepository
    {
        private readonly AppDbContext _context;

        public AuthenticationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> GetUserAsync(SignInRequest signInRequest, CancellationToken cancellationToken)
        {
            var user = await _context.ApplicationUsers
                .Where(u => u.Username == signInRequest.Username)
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                throw new ResourceNotFoundException("User does not exist");
            }

            return user;
        }

        public async Task AddUserAsync(SignUpRequest signUpRequest, CancellationToken cancellationToken)
        {
            var hashedPassword = PasswordSecurity.HashPassword(signUpRequest.UserPassword);
            var newUser = new ApplicationUser()
            {
                Username = signUpRequest.Username,
                UserPassword = hashedPassword,
                UserRole = signUpRequest.IsAdministrator ? UserRole.Administrator : UserRole.Employee
            };

            await _context.ApplicationUsers.AddAsync(newUser, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}