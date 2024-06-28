using System.ComponentModel.DataAnnotations;

namespace Projekt.Models.Financial.Request
{
    public class ProductAgreementPaymentRequest
    {
        [Required]
        public int AgreementId { get; set; }

        public string? PaymentDescription { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public void Validate()
        {
            if (Amount <= 0m)
            {
                throw new ArgumentException("Can not do that");
            }

            if (decimal.Round(Amount, 2) != Amount)
            {
                throw new ArgumentException("The Amount can have maximum 2 decimal places");
            }
        }
    }
}
