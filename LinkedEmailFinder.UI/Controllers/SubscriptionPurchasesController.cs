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
using Microsoft.EntityFrameworkCore;

namespace LinkedEmailFinder.UI.Controllers
{
    public class SubscriptionPurchasesController : Controller
    {
        private readonly LinkedInEmailFinder_DBContext db;
        private readonly IRepository<SubscriptionPurchasesListViewModel> splRepo;

        public SubscriptionPurchasesController(LinkedInEmailFinder_DBContext db, IRepository<SubscriptionPurchasesListViewModel> splRepo)
        {
            this.db = db;
            this.splRepo = splRepo;
        }
        public async Task<IActionResult> Index()
        {
            var list = await db.SubscriptionPurchasesListViewModel.FromSqlInterpolated($"EXEC SubscriptionPurchasesList").ToListAsync();
            return View(list);
        }

        public IActionResult disable(string purchaseid, bool mode)
        {
            var result = db.Database.
                ExecuteSqlInterpolated($"EXEC UpdateSubscriptionPurchaseActive @purchaseid = {purchaseid}, @mode={mode}");
            if (result > 0)
            {
                return RedirectToAction("index");
            }
            else
            {
                ModelState.AddModelError("", "Error occured while disabling the subcription");
                return RedirectToAction("index");
            }
            return RedirectToAction("index");
        }
    }
}