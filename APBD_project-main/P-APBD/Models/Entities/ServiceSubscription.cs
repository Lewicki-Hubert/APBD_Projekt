namespace Projekt.Entities
{
    public class ServiceSubscription
    {
        public int SubscriptionId { get; set; }
        public int SoftwareId { get; set; }
        public string SubscriptionName { get; set; }
        public int RenewalPeriodInMonths { get; set; }
        public decimal SubscriptionCost { get; set; }
        public virtual Software Software { get; set; }
    }
}