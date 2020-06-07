using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class RegistrationViewModel
    {
        [Required]
        [RegularExpression("[a-zA-Z]{5,}", ErrorMessage = "UserName Must be More than 5 Characters")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter and one number")]

        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

       
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }
}