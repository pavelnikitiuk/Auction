using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Auction.Domain.Abstract;
using Auction.Domain.Entities;

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
        /// <summary>
        /// Application main page
        /// </summary>
        /// <returns>Main page</returns>
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
        /// <summary>
        /// About action
        /// </summary>
        /// <returns>About page</returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        /// <summary>
        /// Contact action
        /// </summary>
        /// <returns>Contact page</returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}