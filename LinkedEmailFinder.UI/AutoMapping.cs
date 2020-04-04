using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using LinkedInEmailFinder.Models.UserFields;
 using LinkedInEmailFinder.Models.ViewModels;


namespace LinkedEmailFinder.UI
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ApplicationUser, UserViewModel>();
            CreateMap<UserViewModel, ApplicationUser>();
            CreateMap<ApplicationUser, PasswordChange>();
        }
    }
}
