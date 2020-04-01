using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LinkedInEmailFinder.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace LinkedEmailFinder.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<IdentityUser> smanager;

        public LoginController(SignInManager<IdentityUser> smanager)
        {
            this.smanager = smanager;
        }
      public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login( LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await smanager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if(result.Succeeded)
                {
                    ViewBag.IsAuthenticated = User.Identity.IsAuthenticated;
                    return RedirectToAction("index", "home");
                }
                ModelState.AddModelError("", "Login failed");
            }
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await smanager.SignOutAsync();
            return RedirectToAction("index", "home");

        }
    }
}