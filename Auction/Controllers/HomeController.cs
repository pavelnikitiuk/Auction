using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Auction.Domain.Abstract;
using Auction.Domain.Entities;
using Auction.Models;

namespace Auction.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {

        private ILotsRepository repository;

        public HomeController(ILotsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<Lot> models = new List<Lot>();
            models.AddRange(repository.Lots.Where(x => x.IsCompleted == false)
                .OrderBy(x => x.EndTime).Take(7));
            if(models.Any())
                return View(models);
            return RedirectToAction("About","Home");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}