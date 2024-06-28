namespace Projekt.Entities
{
    public class SoftwareContract
    {
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
        public virtual Customer Customer { get; set; }
        public virtual Software Software { get; set; }
        public virtual Promotion? Promotion { get; set; }
        public virtual ICollection<ContractPayment> ContractPayments { get; set; }
    }
}