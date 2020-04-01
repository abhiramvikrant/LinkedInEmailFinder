using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LinkedInEmailFinder.Models.ViewModels
{
  public   class RegisterViewModel
    {
        [Required][EmailAddress]
        public string Email { get; set; }
        [Required][DataType(DataType.Password)]
        public string Password { get; set; }

        [Required][DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage ="Password and Confirm password doesn't match")]
        public string ConfirmPassword { get; set; }

    }
}
