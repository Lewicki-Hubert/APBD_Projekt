using Projekt.Models.Authentication;
using Projekt.Models.Entities;

namespace Projekt.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<ApplicationUser> GetUserAsync(SignInRequest signInRequest, CancellationToken cancellationToken);
        Task AddUserAsync(SignUpRequest signUpRequest, CancellationToken cancellationToken);
    }
}