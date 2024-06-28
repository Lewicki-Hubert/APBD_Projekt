using Projekt.Enums;

namespace Projekt.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public CustomerType Type { get; set; }
        public bool IsDeprecated { get; set; }
        public virtual Company Company { get; set; }
        public virtual Individual Individual { get; set; }
        public virtual ICollection<SoftwareContract> SoftwareContracts { get; set; }
    }
}