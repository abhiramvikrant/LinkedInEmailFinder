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
using Microsoft.EntityFrameworkCore;

namespace LinkedEmailFinder.UI.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> rmanager;
        private readonly UserManager<IdentityUser> umanager;

        public IActionResult Index()
        {
            var rolelist = rmanager.Roles.ToList(); ;
            return View(rolelist);
        }
        public AdministrationController(RoleManager<IdentityRole> rmanager, UserManager<IdentityUser> umanager)
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
        public async Task<IActionResult> CreateRole(RoleCreateViewModel model)
        {
            if (ModelState.IsValid)
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
       
        [HttpGet]
        public async Task<IActionResult> ListUsersInRole(string roleid)
        {

            var role = await rmanager.FindByIdAsync(roleid.ToString());
            if (role == null)
            {

                ModelState.AddModelError("", "Invalid role");
                return View();
            }
            ViewBag.RoleName = role.Name;

            var ulist = umanager.Users.ToList();
            var users = new UserRoles();
            var userlist = new List<UserRoles>();
            foreach (var item in ulist)
            {
                var u = new UserRoles()
                {
                    UserName = item.UserName,
                    UserId = item.Id,
                    IsInRole =
                    await umanager.IsInRoleAsync(new IdentityUser()
                    { UserName = item.UserName, Id = item.Id }, role.Name) ? true : false
                };
                
                userlist.Add(u);          
         }


            return View(userlist);
        }

        [HttpPost]
        public async Task<IActionResult> ListUsersInRole(List<UserRoles> models, string roleid)
        {
            var role = await rmanager.FindByIdAsync(roleid.ToString());
            List<IdentityResult> results = new List<IdentityResult>();

     
            foreach (var item in models)
            {
                var user = await umanager.FindByIdAsync(item.UserId);
                //IdentityUser u = new IdentityUser() { UserName = item.UserName, Id = item.UserId };
                if ((item.IsInRole) && (!await umanager.IsInRoleAsync(user, role.Name)))
                {

                    
                    await umanager.AddToRoleAsync(user, role.Name);


                }

                if ((!item.IsInRole) && (await umanager.IsInRoleAsync(user, role.Name)))
                {

                    await umanager.RemoveFromRoleAsync(user, role.Name);

                }

                for (int i = 0; i < results.ToList().Count; i++)
                {
                    if (!results[i].Succeeded)
                        ModelState.AddModelError("", "Error occured");
                }
                
            }
            return View(models);
        }

    }
}