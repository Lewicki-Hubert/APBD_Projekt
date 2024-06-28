using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Projekt.Models.Financial.Request
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