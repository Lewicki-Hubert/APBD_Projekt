using System.ComponentModel.DataAnnotations;

namespace Projekt.Models.Customer.Request
{
    public class CompanyAddRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string KRS { get; set; }
    }
}