using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LinkedEmailFinder.DataAccess;
using LinkedEmailFinder.DataAccess.Repository;
using LinkedInEmailFinder.Models;

namespace LinkedEmailFinder.UI.Controllers
{
    public class SubscriptionsController : Controller
    {
        private readonly IRepository<Subscriptions> srepo;
        private readonly LinkedInEmailFinder_DBContext dbcontext;

        public SubscriptionsController(IRepository<Subscriptions> srepo, LinkedInEmailFinder_DBContext dbcontext)
        {
            this.srepo = srepo;
            this.dbcontext = dbcontext;
        }
        public IActionResult Index()
        {
            var sublist = srepo.GetAll().ToList();
            return View(sublist);
        }
    }
}