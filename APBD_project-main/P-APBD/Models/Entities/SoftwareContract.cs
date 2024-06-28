using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Entities
{
    [Table("SoftwareContracts")]
    public class SoftwareContract
    {
        [Key]
        public int ContractId { get; set; }
        public int CustomerId { get; set; }
        public int SoftwareId { get; set; }
        public string SoftwareVersion { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UpdateDescription { get; set; }
        public int SupportDuration { get; set; }
        public int? PromotionId { get; set; }
        public int? DiscountValue { get; set; }
        public decimal TotalCost { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("SoftwareId")]
        public virtual Software Software { get; set; }
        [ForeignKey("PromotionId")]
        public virtual Promotion? Promotion { get; set; }
        public virtual ICollection<ContractPayment> ContractPayments { get; set; }
    }
}