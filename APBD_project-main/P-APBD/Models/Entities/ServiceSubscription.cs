using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models.Entities
{
    [Table("ServiceSubscriptions")]
    public class ServiceSubscription
    {
        [Key]
        public int SubscriptionId { get; set; }
        public int SoftwareId { get; set; }
        public string SubscriptionName { get; set; }
        public int RenewalPeriodInMonths { get; set; }
        public decimal SubscriptionCost { get; set; }
        
        [ForeignKey("SoftwareId")]
        public virtual Software Software { get; set; }
    }
}