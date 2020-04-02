using System;
using System.Collections.Generic;
using System.Text;
using LinkedInEmailFinder.Models;
using LinkedInEmailFinder.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace LinkedInEmailFinder.UI.ViewModels
{
   public class UserToRoleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<IdentityRole> UsersInRole { get; set; }
    }
}
