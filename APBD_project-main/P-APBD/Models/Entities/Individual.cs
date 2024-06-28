using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Entities
{
    [Table("Individuals")]
    public class Individual
    {
        [Key, ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? HomeAddress { get; set; }
        public string? EmailAddress { get; set; }
        public string? MobileNumber { get; set; }
        public string? NationalId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}