using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LinkedInEmailFinder.Models.ViewModels
{
    public class UserViewModel
    {[Key]
        public string Id { get; set; }
        [Required]
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string UserName { get; set; }
        [Required]
        [Display(Name = " Address 1")]
        public string Address1 { get; set; }
        [Display(Name = " Address 2")]
        public string Address2 { get; set; }
        [Required]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }
        public string City { get; set; }

        public int StateId { get; set; }
        public int CountryId { get; set; }
        [Display(Name= "Pin Code")]
        public int PinCode { get; set; }
        [Display(Name = " Say about yourself")]
        public string SayAboutYourSelf { get; set; }

        public int PurposeId { get; set; }
    }
}
