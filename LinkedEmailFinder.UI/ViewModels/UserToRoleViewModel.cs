using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;


namespace LinkedInEmailFinder.UI.ViewModels
{
   public class UserToRoleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<UserRoles> UserRoleList{ get; set; }

        public bool IsSelected { get; set; }
        public string RoleName { get; set; }
        public string UserId { get; set; }

        public string UserName { get; set; }

        public bool IsInRole { get; set; }
    }
}
