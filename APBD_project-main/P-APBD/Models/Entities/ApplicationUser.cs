using Projekt.Enums;

namespace Projekt.Entities
{
    public class ApplicationUser
    {
        public string Username { get; set; }
        public string UserPassword { get; set; }
        public UserRole UserRole { get; set; }
    }
}