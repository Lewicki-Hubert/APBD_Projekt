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
                throw new ArgumentException("End date cannot be earlier than start date.");
            }
            int dayDifference = (EndDate - StartDate).Days;
            if (dayDifference < 3 || dayDifference > 30)
            {
                throw new ArgumentException("The time difference between dates must be between 3 and 30 days.");
            }
        }
        
    }
}