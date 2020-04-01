using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNet.Identity.EntityFramework;


namespace LinkedInEmailFinder.Models.ViewModels
{
   public  class RolesListViewModel
    {
        public List<IdentityRole> RoleList { get; set; }
    }
}
