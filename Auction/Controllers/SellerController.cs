using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Auction.Domain.Abstract;
using Auction.Domain.DBase;
using Auction.Domain.Entities;
using Auction.Models;

namespace Auction.Controllers
{
    [Authorize(Roles = "seller")]
    public class SellerController : Controller
    {
        private ILotsRepository lotsRepository;
        // ReSharper disable once InconsistentNaming
        private ICategoriesRepository categoriesRepository;
        public SellerController(ILotsRepository lotsRepository, ICategoriesRepository categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
            this.lotsRepository = lotsRepository;
        }
        //
        // GET: /Seller/
        [HttpGet]
        public ActionResult Sell()
        {

            ViewBag.Categories = categoriesRepository.Categories.Select(x => x.CategoryName).OrderBy(x => x);
            return View(new Lot
            {
                EndTime = DateTime.Now.AddHours(1),
                MinPrice = 1
            });
        }

        [HttpPost]
        public ActionResult Sell(Domain.Entities.Lot model, HttpPostedFileBase[] image, string categ)
        {
            ViewBag.Categories = categoriesRepository.Categories.Select(x => x.CategoryName).OrderBy(x => x);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (!ModelState.IsValid || !categoriesRepository.Categories.Any(x => x.CategoryName == categ))
                return View(model);
            model.Images = new List<Image>();
            int i = 0;
            foreach (var img in image)
            {
                if (img != null)
                {
                    model.Images.Add(new Image()
                    {
                        ImageMimeType = img.ContentType,
                        ImageData = new byte[img.ContentLength],
                    });
                    img.InputStream.Read(model.Images[i++].ImageData, 0, img.ContentLength);
                }
            }
            model.CurrentPrice = model.MinPrice;
            model.IsCompleted = false;
            var cat = categoriesRepository.Categories.FirstOrDefault(x => x.CategoryName == categ);
            categoriesRepository.AddLot(cat, model);
            return RedirectToAction("Lot", "Lots",new {lotId= model.LotID});
        }
    }
}