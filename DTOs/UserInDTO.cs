﻿using EcommerceTask.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceTask.DTOs
{
    public class UserInDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$")]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"^\\+?[1-9][0-9]{7,14}$")]
        public string PhoneNumber { get; set; }
    }
}