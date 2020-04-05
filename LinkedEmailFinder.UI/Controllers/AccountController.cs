using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LinkedInEmailFinder.Models;
using LinkedInEmailFinder.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using LinkedInEmailFinder.Models.UserFields;
using LinkedEmailFinder.DataAccess;
using LinkedInEmailFinder.Email;
using AutoMapper;


namespace LinkedEmailFinder.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> umanager;
        private readonly SignInManager<ApplicationUser> smanager;
        private readonly IMapper mapper;
        private readonly LinkedInEmailFinder_DBContext db;

        public AccountController(UserManager<ApplicationUser> umanager, SignInManager<ApplicationUser> smanager, IMapper mapper, LinkedInEmailFinder_DBContext db)
        {
            this.umanager = umanager;
            this.smanager = smanager;
            this.mapper = mapper;
            this.db = db;
            db.Countries.ToList();
        }

        public IActionResult Index()
        {
            return View();
        }
    

        public async Task<IActionResult> SignOut()
        {
            await smanager.SignOutAsync();
            return View();
        }
 
        [HttpGet]
        public async Task<IActionResult> EditUser(string userid)
        {
            var userbyid = await umanager.FindByIdAsync(userid);
            // ViewBag.CountryList = db.Countries.Where(a => a.IsActive == true).ToList();
            if (userbyid == null)
            {
                ModelState.AddModelError("", "User not found");
            }
            //UserViewModel u = new UserViewModel() { Id = userbyid.Id, UserName = userbyid.UserName };
            UserViewModel u = mapper.Map<UserViewModel>(userbyid);
            return View(u);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                return await SaveUser(user, true);
            }
            return View(user);
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
       

        private async Task<IActionResult> SaveUser(UserViewModel user, bool goToUserList)
        {
            ApplicationUser u = await MapManually(user);
            var ret = await umanager.UpdateAsync(u);
            
            if (ret == null)
            {
                ModelState.AddModelError("", "User not found");
            }
            if (goToUserList)
            {
                return RedirectToAction("userlist");
            }
            else if(goToUserList == false)
            {
                return RedirectToAction("index", "home");
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserByUserName(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                return await SaveUser(user, false);
            }
            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> EditUserByUserName(string username)
        {
            var userbyid = await umanager.FindByNameAsync(username);
           // ViewBag.CountryList = db.Countries.Where(a => a.IsActive == true).ToList();
            if (userbyid == null)
            {
                ModelState.AddModelError("", "User not found");
            }
            //UserViewModel u = new UserViewModel() { Id = userbyid.Id, UserName = userbyid.UserName };
            UserViewModel u = mapper.Map<UserViewModel>(userbyid);
            return View(u);

        }
        [HttpGet]
        public async  Task<IActionResult>ChangePassword(string username)
        {
            var user = await umanager.FindByNameAsync(username);
            var p = mapper.Map<PasswordChange>(user);
            return View(p);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(PasswordChange P)
        {
            if (ModelState.IsValid) {
            var user = await umanager.FindByNameAsync(P.UserName);
            var result = umanager.ChangePasswordAsync(user, P.OldPassword, P.NewPassword);
            if (result.Result.Succeeded)
            {
                return RedirectToAction("index", "home");
            }
                                    }
            return View(P);

        }

        private async Task<ApplicationUser> MapManually(UserViewModel user)
        {
            var u = await umanager.FindByIdAsync(user.Id);
            u.FirstName = user.FirstName;
            u.LastName = user.LastName;
            u.Address1 = user.Address1;
            u.Address2 = user.Address2;
            u.CountryId = user.CountryId;
            u.MobileNumber = user.MobileNumber;
            u.PinCode = user.PinCode;
            u.PurposeId = user.PurposeId;
            u.StateId = user.StateId;
            u.SayAboutYourSelf = user.SayAboutYourSelf;
            return u;
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


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswodViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await umanager.FindByNameAsync(model.Email);
                var token = await umanager.GeneratePasswordResetTokenAsync(user);
                var resetlink = Url.Action("resetpassword", "account", new { email = user, token = token }, Request.Scheme);
                // SendEmail(model.Email, resetlink);
                ViewBag.Title = "Reset Password";
                ViewBag.Message = "An email message was sent to your registered email with the reset link. Kindly click the " +
                    "mentioned in the email for resetting the password";
                return View("ShowMessage");

            }
            ViewBag.Title = "Reset Password";
            ViewBag.Message = "Error occured while resetting the password";
            return View("ShowMessage");
        }
    

        private void SendEmail(string ToEmail, string EmailMessage)
        {
            EmailSupport support = new EmailSupport() { To = ToEmail, Subject = "Reset Password Link",
                Message  = EmailMessage};
            try
            {
                support.SendEmail();
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
            }

        }
        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {

            var user = await umanager.FindByNameAsync(model.Email);
            var result = await umanager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if(result.Succeeded)
            {
                ViewBag.Title = "Passord reset succedded";
                ViewBag.Message = "Password reset was successfully performed.";
                return View("ShowMessage");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

            }
            return View(model);
        }
      
    }
}