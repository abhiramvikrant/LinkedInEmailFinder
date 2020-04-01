using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LinkedInEmailFinder.Models;
using LinkedInEmailFinder.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace LinkedEmailFinder.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> umanager;
        private readonly SignInManager<IdentityUser> smanager;

        public AccountController(UserManager<IdentityUser> umanager, SignInManager<IdentityUser> smanager)
        {
            this.umanager = umanager;
            this.smanager = smanager;
      
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> SignOut()
        {
            await smanager.SignOutAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser(model.Email);
                var result = await umanager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await smanager.PasswordSignInAsync(model.Email, model.Password, false, false);
                   bool r =  smanager.IsSignedIn(User);
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                   return  RedirectToAction("index","home");
                }
               
            

            }
            return View(model);

        }
    }
}