using Projekt.Entities;
using Projekt.Models.Login;

namespace Projekt.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<ApplicationUser> GetUserAsync(SignInRequest signInRequest, CancellationToken cancellationToken);
        Task AddUserAsync(SignUpRequest signUpRequest, CancellationToken cancellationToken);
    }
}