using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LinkedInEmailFinder.Models.ViewModels
{
   public class ForgotPasswodViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
