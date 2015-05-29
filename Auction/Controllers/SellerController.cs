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
        public ViewResult Sell()
        {
            var cat = categoriesRepository.Categories.Select(x => x.CategoryName).OrderBy(x => x);
            IEnumerable<SelectListItem> list = (from category in cat select new SelectListItem { Text = category }).ToList();
            return View(new SellModel
            {
                MinPrice = 1,
                Categories =  list
            });
        }
        [HttpPost]
        public ActionResult Sell(SellModel model, string categ)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (!ModelState.IsValid)
            {
                var c = categoriesRepository.Categories.Select(x => x.CategoryName).OrderBy(x => x);
                model.Categories = (from category in c select new SelectListItem { Text = category }).ToList();
                
                return View(model);
            }
            Lot lot = new Lot
            {
                Images = new List<Image>(),
                CurrentPrice =  model.MinPrice,
                Name = model.Name,
                MinPrice = model.MinPrice,
                Description = model.Description,
                EndTime = model.EndTime,
                IsCompleted = false
            };
            int i = 0;
            foreach (var img in model.Files)
            {
                if (img != null)
                {
                    lot.Images.Add(new Image()
                    {
                        ImageMimeType = img.ContentType,
                        ImageData = new byte[img.ContentLength],
                    });
                    img.InputStream.Read(lot.Images[i++].ImageData, 0, img.ContentLength);
                }
            }
            
            var cat = categoriesRepository.Categories.FirstOrDefault(x => x.CategoryName == categ);
            categoriesRepository.AddLot(cat, lot);
            return RedirectToAction("Lot", "Lots", new { lotId = lot.LotID });
        }
    }
}