using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Projekt.Enums;

namespace Projekt.Entities
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public CustomerType Type { get; set; }
        public bool IsDeprecated { get; set; }
        public virtual Company Company { get; set; }
        public virtual Individual Individual { get; set; }
        public virtual ICollection<SoftwareContract> SoftwareContracts { get; set; }
    }
}