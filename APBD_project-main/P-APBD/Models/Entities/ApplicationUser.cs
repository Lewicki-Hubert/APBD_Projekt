using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Projekt.Models.Base;

namespace Projekt.Models.Entities
{
    [Table("ApplicationUsers")]
    public class ApplicationUser
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string UserPassword { get; set; }
        public UserRole UserRole { get; set; }
    }
}