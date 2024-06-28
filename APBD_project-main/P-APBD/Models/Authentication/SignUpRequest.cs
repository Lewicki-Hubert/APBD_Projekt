using System.ComponentModel.DataAnnotations;

namespace Projekt.Models.Authentication
{
    public class SignUpRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string UserPassword { get; set; }

        [Required]
        public bool IsAdministrator { get; set; }
    }
}