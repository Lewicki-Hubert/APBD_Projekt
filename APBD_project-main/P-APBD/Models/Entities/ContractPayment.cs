namespace Projekt.Entities
{
    public class ContractPayment
    {
        public int PaymentId { get; set; }
        public int ContractId { get; set; }
        public string? PaymentDescription { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public virtual SoftwareContract SoftwareContract { get; set; }
    }
}