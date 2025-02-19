﻿
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LinkedInEmailFinder.Models.UserFields
{
   public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {

        }

        public ApplicationUser(string email)
        {
            base.Email = email;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string MobileNumber { get; set; }
        public string City { get; set; }

        public int StateId { get; set; }
        public int CountryId { get; set; }

        public int PinCode { get; set; }
     
        public string SayAboutYourSelf { get; set; }

        public int PurposeId { get; set; }
    }
}
