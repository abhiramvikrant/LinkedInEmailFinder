using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LinkedInEmailFinder.Models.ViewModels
{
    public class ResetPasswordViewModel
    {
        public string Email { get; set; }
        public string Token { get; set; }
        [Required(ErrorMessage = "New password is a required field")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm password is a required field")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage ="Confirm password doesn't match")]
        public string ConfirmPassword { get; set; }
    }
}
