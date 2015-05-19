using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Auction.Domain.Abstract;
using Auction.Domain.Entities;

namespace Auction.Controllers
{
    [Authorize]
    public class SellerController : Controller
    {
        private ILotsRepository lotsRepository;
        public SellerController(ILotsRepository lotsRepository)
        {
            this.lotsRepository = lotsRepository;
        }
        //
        // GET: /Seller/
        [HttpGet]
        public ActionResult Sell()
        {
            return View(new Lot(){EndTime = DateTime.Now});
        }

        [HttpPost]
        public ActionResult Sell(Domain.Entities.Lot model, HttpPostedFileBase image)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (image != null)
            {
                model.ImageMimeType = image.ContentType;
                model.ImageData = new byte[image.ContentLength];
                image.InputStream.Read(model.ImageData, 0, image.ContentLength);
            }
            model.CurrentPrice = model.MinPrice;
            model.IsCompleted = false;
            lotsRepository.AddLot(model);
            return RedirectToAction("List", "Lots");
        }
	}
}