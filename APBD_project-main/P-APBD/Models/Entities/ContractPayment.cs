using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Entities
{
    [Table("ContractPayments")]
    public class ContractPayment
    {
        [Key]
        public int PaymentId { get; set; }
        public int ContractId { get; set; }
        public string? PaymentDescription { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        [ForeignKey("ContractId")]
        public virtual SoftwareContract SoftwareContract { get; set; }
    }
}