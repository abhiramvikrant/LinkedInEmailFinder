using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LinkedInEmailFinder.Models;
using LinkedInEmailFinder.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using LinkedInEmailFinder.UI.ViewModels;

namespace LinkedEmailFinder.UI.Controllers
{
 [Authorize]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> rmanager;
        private readonly UserManager<IdentityUser> umanager;

        public IActionResult Index()
        {
            var rolelist = rmanager.Roles.ToList(); ;
            return View(rolelist);
        }
        public AdministrationController( RoleManager<IdentityRole> rmanager, UserManager<IdentityUser> umanager)
        {
            this.rmanager = rmanager;
            this.umanager = umanager;
            
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole( RoleCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();
                role.Name = model.Name;
                var result = await rmanager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "administration");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
           
        }

        public async Task<IActionResult> ListUsersInRole(int id)
        {
            var role = rmanager.FindByIdAsync(id.ToString());
            if(role == null)
            {
                
                ModelState.AddModelError("", "Invalid role");
            }
            UserToRoleViewModel model = new UserToRoleViewModel();
            
          


            var userlist = umanager.Users;

            foreach (var item in userlist)
            {

            }
            return View();
        }

        }
    }
