using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Projekt.Models.Payment.Request
{
    public class IncomeRequest
    {
        [Required]
        [FromQuery]
        public DateTime StartDate { get; set; }

        [Required]
        [FromQuery]
        public DateTime EndDate { get; set; }

        public void Validate()
        {
            if (EndDate < StartDate)
            {
                throw new ArgumentException("It is not possible");
            }
        }
    }
}