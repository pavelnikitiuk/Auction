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

            return View(new SellModel
            {
                Lot = new Lot
                {
                    MinPrice = 1
                },
                Categories = categoriesRepository.Categories.Select(x => x.CategoryName).OrderBy(x => x)
            });
        }

        [HttpPost]
        public ActionResult Sell(SellModel model, string categ)
        {
            //var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (!ModelState.IsValid )
                return View(new SellModel
                {
                    Lot = model.Lot,
                    Categories = categoriesRepository.Categories.Select(x => x.CategoryName).OrderBy(x => x)
                });
            model.Lot.Images = new List<Image>();
            int i = 0;
            foreach (var img in model.Files)
            {
                if (img != null)
                {
                    model.Lot.Images.Add(new Image()
                    {
                        ImageMimeType = img.ContentType,
                        ImageData = new byte[img.ContentLength],
                    });
                    img.InputStream.Read(model.Lot.Images[i++].ImageData, 0, img.ContentLength);
                }
            }
            model.Lot.CurrentPrice = model.Lot.MinPrice;
            model.Lot.IsCompleted = false;
            var cat = categoriesRepository.Categories.FirstOrDefault(x => x.CategoryName == categ);
            categoriesRepository.AddLot(cat, model.Lot);
            return RedirectToAction("Lot", "Lots", new { lotId = model.Lot.LotID });
        }
    }
}