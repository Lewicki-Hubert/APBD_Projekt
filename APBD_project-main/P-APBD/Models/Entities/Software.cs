namespace Projekt.Entities
{
    public class Software
    {
        public int SoftwareId { get; set; }
        public string SoftwareName { get; set; }
        public string SoftwareDescription { get; set; }
        public string SoftwareVersion { get; set; }
        public decimal AnnualCost { get; set; }
        public virtual ICollection<SoftwareContract> SoftwareContracts { get; set; }
        public virtual ICollection<ServiceSubscription> ServiceSubscriptions { get; set; }
    }
}