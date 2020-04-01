using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedInEmailFinder.Models.UserFields
{
   public class ApplicationUser
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string MobileNumber { get; set; }
        public string City { get; set; }

        public string State { get; set; }
        public string Country { get; set; }

        public int PinCode { get; set; }
    }
}
