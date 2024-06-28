using System.ComponentModel.DataAnnotations;

namespace Projekt.Models.Login
{
    public class SignInRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string UserPassword { get; set; }
    }
}