using Microsoft.IdentityModel.Tokens;
using Projekt.Repositories;
using Projekt.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Projekt.Models.Authentication;
using Projekt.Models.Entities;

namespace Projekt.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IAuthenticationRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string> SignInAsync(SignInRequest signInRequest, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserAsync(signInRequest, cancellationToken);
            PasswordSecurity.CheckPassword(signInRequest.UserPassword, user.UserPassword);
            return GenerateJwtToken(user);
        }

        public async Task SignUpAsync(SignUpRequest signUpRequest, CancellationToken cancellationToken)
        {
            await _userRepository.AddUserAsync(signUpRequest, cancellationToken);
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.UserRole.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
