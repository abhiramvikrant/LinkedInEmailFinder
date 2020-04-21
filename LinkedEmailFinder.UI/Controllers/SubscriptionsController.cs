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
using Microsoft.AspNetCore.Authorization;
using Stripe.Infrastructure;

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
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string subid)
        {
            var objSub = srepo.GetAll().Where(s => s.SubscriptionId.ToString() == subid).FirstOrDefault();
            return View(objSub);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
    
       [Authorize(Roles="Admin")]
        public IActionResult Edit(Subscriptions s)
        {
            var result = srepo.Update(s); 
            return RedirectToAction("AdminIndex");

        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string subid)
        {

            var objSub = srepo.GetAll().Where(s => s.SubscriptionId.ToString() == subid).FirstOrDefault();
            var result = srepo.Delete(objSub);
            return RedirectToAction("AdminIndex");

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
            // set public key
            Stripe.StripeConfiguration.ApiKey = "pk_test_T0VwRvul36mHa8GBORL45kgE00gkwVwv5a";
            Stripe.CreditCardOptions card = new Stripe.CreditCardOptions();
            card.Name = $"{user.FirstName} {user.LastName}";
            card.Number = "4242424242424242";
            card.ExpYear = 2022;
            card.ExpMonth = 10;
            card.Cvc = "123";
            var roptions = new Stripe.RequestOptions
            {
                //set secret key
                ApiKey = "sk_test_QJLhsrOV1aW0pWQ7t2WwIU7y00UKAt8ud8"
            };
          

            Stripe.TokenCreateOptions token = new Stripe.TokenCreateOptions();
            token.Card = card;
            Stripe.TokenService serviceToken = new Stripe.TokenService();
           Stripe.Token newToken =  serviceToken.Create(token);
            Stripe.CustomerCreateOptions myCustomer = new Stripe.CustomerCreateOptions();
            myCustomer.Email = user.Email;
            myCustomer.Source = newToken.Id;
            myCustomer.Name = $"{user.FirstName} {user.LastName}";
            Stripe.AddressOptions homeOptions = new Stripe.AddressOptions
            {
                City = user.City,
                PostalCode = user.PinCode.ToString(),
                Country = "India",
                Line1 = user.Address1,
                Line2 = user.Address2,
                State = "Tamil Nadu"               
            };

            myCustomer.Address = homeOptions;

            var customerService = new Stripe.CustomerService();
            
            Stripe.Customer stripeCustomer = customerService.Create(myCustomer,roptions);
            var options = new Stripe.ChargeCreateOptions {
                Amount = 1000,
                Currency = "INR",
                ReceiptEmail = user.Email,               
                Description = "Ammount paid" ,
                Customer  = stripeCustomer.Id,

            };
            var service = new Stripe.ChargeService();
            Stripe.Charge charge = service.Create(options,roptions);
            if(charge.Status == "succeeded")
            {
                ViewBag.Message = "Payment succeded";
            }


            

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
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(SubscriptionCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var obj = mapper.Map<Subscriptions>(model);
                var result = srepo.Create(obj);
                if (result > 0)
                {
                    return RedirectToAction("AdminIndex");
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