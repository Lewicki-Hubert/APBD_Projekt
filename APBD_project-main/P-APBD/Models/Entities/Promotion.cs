namespace Projekt.Entities
{
    public class Promotion
    {
        public int PromotionId { get; set; }
        public string PromotionName { get; set; }
        public int DiscountValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual ICollection<SoftwareContract> SoftwareContracts { get; set; }
    }
}