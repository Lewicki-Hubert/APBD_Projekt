using System.ComponentModel.DataAnnotations;

namespace Projekt.Models.Agreement
{
    public class ProductAgreementAddRequest
    {
        [Required]
        public int ClientId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string UpdateDescription { get; set; }

        [Required]
        public int SupportExtensionYears { get; set; }

        public void Validate()
        {
            if (EndDate < StartDate)
            {
                throw new ArgumentException("It is not possible");
            }
        }
    }
}