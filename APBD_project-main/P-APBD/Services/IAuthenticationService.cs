using Projekt.Models.Login;

namespace Projekt.Services
{
    public interface IAuthenticationService
    {
        Task<string> SignInAsync(SignInRequest signInRequest, CancellationToken cancellationToken);
        Task SignUpAsync(SignUpRequest signUpRequest, CancellationToken cancellationToken);
    }
}