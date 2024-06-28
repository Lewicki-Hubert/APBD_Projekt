﻿using System.ComponentModel.DataAnnotations;

namespace Projekt.Models.Client.Request
{
    public class IndividualAddRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string PESEL { get; set; }
    }
}