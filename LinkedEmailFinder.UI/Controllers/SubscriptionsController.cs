using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LinkedEmailFinder.DataAccess;
using LinkedEmailFinder.DataAccess.Repository;
using LinkedInEmailFinder.Models;
using Microsoft.AspNetCore.Identity;
using LinkedInEmailFinder.Models.UserFields;
using LinkedInEmailFinder.Models.ViewModels;
using AutoMapper;

namespace LinkedEmailFinder.UI.Controllers
{
    public class SubscriptionsController : Controller
    {
        private readonly IRepository<Subscriptions> srepo;
        private readonly UserManager<ApplicationUser> umanager;
        private readonly IRepository<SubscriptionPurchases> purRepo;
        private readonly LinkedInEmailFinder_DBContext dbcontext;
        private readonly IMapper mapper;

        public SubscriptionsController(IRepository<Subscriptions> srepo,UserManager<ApplicationUser> umanager,
            IRepository<SubscriptionPurchases> purRepo, LinkedInEmailFinder_DBContext dbcontext, IMapper mapper)
        {
            this.srepo = srepo;
            this.umanager = umanager;
            this.purRepo = purRepo;
            this.dbcontext = dbcontext;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult AdminIndex()
        {
            var sublist = srepo.GetAll().OrderBy(c => c.SubscriptionDisplayOrder).ToList();
            return View(sublist);
        
        }

        [HttpPost]
        public IActionResult Edit(Subscriptions s)
        {
            var result = srepo.Update(s); return View("AdminIndex");

        }
        public IActionResult Delete(string subid)
        {

            var objSub = srepo.GetAll().Where(s => s.SubscriptionId.ToString() == subid).FirstOrDefault();
            var result = srepo.Delete(objSub);
            return View("AdminIndex");

        }
       [HttpGet]
        public IActionResult Index()
        {
            var sublist = srepo.GetAll().Where(s=> s.IsActive == true).OrderBy(c => c.SubscriptionDisplayOrder).ToList();
            return View(sublist);
        }
        [HttpGet]
        public IActionResult ConfirmPurchase(string subscriptionid)
        {
            Subscriptions model = GetSubscriptionById(subscriptionid);
            return View(model);
        }

        private Subscriptions GetSubscriptionById(string subscriptionid)
        {
            return srepo.GetAll().Where(s => s.SubscriptionId.ToString() == subscriptionid).FirstOrDefault();
        }
        [HttpPost("{Controller}/confirmpurchase/{subscriptionid}/{username}/{subname}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmPurchase(string subscriptionid, string username, string subname)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error Occured while purchasing");
                return View();
            }
            var model = GetSubscriptionById(subscriptionid);
            var user = await umanager.FindByNameAsync(username);


            SubscriptionPurchases purchase = new SubscriptionPurchases();
            purchase.SubscriptionUID = model.SubscriptionId;
            purchase.UserId = user.Id;
            purchase.PurchasedOn = DateTime.Now;
            purchase.StartDate = DateTime.Now;
            purchase.EndDate = DateTime.Now.AddMonths(model.SubscriptionPeriodInMonths);
            purchase.IsActive = true;
            if (subname.ToLower() == "trial")
            {
                purchase.IsTrial = true;

            }
            else
            {
                purchase.IsTrial = false;
            }
            if ((bool)model.UseDiscountPrice == false)
            {
                purchase.AmountPaid = model.SubscriptionPrice;
            }
            else if ((bool)model.UseDiscountPrice)
            {
                purchase.AmountPaid = (decimal)model.SubscriptionDiscountPrice;
            }

            var result = purRepo.Create(purchase);
            if (result > 0)
            {
                ViewBag.Message = "Purchase completed successfully";
            }
            else
            {
                ModelState.AddModelError("", "Error Occured while purchasing");
            }

            return View();




        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SubscriptionCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var obj = mapper.Map<Subscriptions>(model);
                var result = srepo.Create(obj);
                if (result > 0)
                {
                    return RedirectToAction("index");
                }
                else
                {
                    ModelState.AddModelError("", "Error occured while creating Subscription");
                    return View(model);
                }
            }
            return View(model);
        }
    }
}