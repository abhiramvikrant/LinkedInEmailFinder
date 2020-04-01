using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LinkedInEmailFinder.Models;
using LinkedInEmailFinder.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace LinkedEmailFinder.UI.Controllers
{
 [Authorize]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> rmanager;

        public IActionResult Index()
        {
            var rolelist = rmanager.Roles.ToList(); ;
            return View(rolelist);
        }
        public AdministrationController( RoleManager<IdentityRole> rmanager)
        {
            this.rmanager = rmanager;
            this.rmanager = rmanager;
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
        }
    }
