using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Projekt.Models.Base;

namespace Projekt.Models.Entities
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