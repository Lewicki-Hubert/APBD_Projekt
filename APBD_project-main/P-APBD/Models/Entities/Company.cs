using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models.Entities
{
    [Table("Companies")]
    public class Company
    {
        [Key, ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyPhoneNumber { get; set; }
        public string RegistrationNumber { get; set; }
        
        [ForeignKey("CustomerId")] 
        public virtual Customer Customer { get; set; }
    }
}