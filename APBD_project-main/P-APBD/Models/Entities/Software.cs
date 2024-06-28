using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models.Entities
{
    [Table("Softwares")]
    public class Software
    {
        [Key]
        public int SoftwareId { get; set; }
        public string SoftwareName { get; set; }
        public string SoftwareDescription { get; set; }
        public string SoftwareVersion { get; set; }
        public decimal AnnualCost { get; set; }
        public virtual ICollection<SoftwareContract> SoftwareContracts { get; set; }
        public virtual ICollection<ServiceSubscription> ServiceSubscriptions { get; set; }
    }
}