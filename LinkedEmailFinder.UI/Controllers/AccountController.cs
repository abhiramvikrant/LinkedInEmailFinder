using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LinkedInEmailFinder.Models;
using LinkedInEmailFinder.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using LinkedInEmailFinder.Models.UserFields;


namespace LinkedEmailFinder.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> umanager;
        private readonly SignInManager<ApplicationUser> smanager;

        public AccountController(UserManager<ApplicationUser> umanager, SignInManager<ApplicationUser> smanager)
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
        [HttpGet]
        public async Task<IActionResult>EditUser(string userid)
        {
            var userbyid = await umanager.FindByIdAsync(userid);
            if(userbyid == null)
            {
                ModelState.AddModelError("", "User not found");
            }
            UserViewModel u = new UserViewModel() { Id = userbyid.Id, UserName = userbyid.UserName };
            return View(u);


        }
        [HttpGet]
        public  IActionResult UserList()
        {
            var userlist =  umanager.Users.ToList();
            List<UserViewModel> u = new List<UserViewModel>();
            foreach (var item in userlist)
            {
                u.Add(new UserViewModel() { Id = item.Id, UserName = item.UserName });
            }
            return View(u);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(ApplicationUser user)
        {
            var u = await umanager.FindByIdAsync(user.Id);
            u.UserName = user.UserName;
            var ret = await umanager.UpdateAsync(u);
            if (ret == null)
            {
                ModelState.AddModelError("", "User not found");
            }
            return RedirectToAction("userlist");
        }

        public async Task<IActionResult> DeleteUser(string userid)
        {
            var u = await umanager.FindByIdAsync(userid);
            var ret = await umanager.DeleteAsync(u);
            if (ret == null)
            {
                ModelState.AddModelError("", "User not found");
            }
            return RedirectToAction("userlist");
        }



        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser(model.Email);
                var result = await umanager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await smanager.PasswordSignInAsync(model.Email, model.Password, false, false);
                   var r = await umanager.AddToRoleAsync(user, "Customers");
                   bool ret =  smanager.IsSignedIn(User);
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