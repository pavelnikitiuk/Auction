using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Auction.Domain.Abstract;
using Auction.Domain.DBase;
using Auction.Domain.Entities;
using Auction.Models;
using Auction.Properties;

namespace Auction.Controllers
{
    [Authorize(Roles = "seller")]
    public class SellerController : Controller
    {
        private ILotsRepository lotsRepository;
        private ICategoriesRepository categoriesRepository;

        public SellerController(ILotsRepository lotsRepository, ICategoriesRepository categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
            this.lotsRepository = lotsRepository;
        }

        /// <summary>
        /// Sell get action
        /// </summary>
        /// <returns>Page to sell lot</returns>
        [HttpGet]
        public ViewResult Sell()
        {
            var cat = categoriesRepository.Categories.Select(x => x.CategoryName).OrderBy(x => x);
            IEnumerable<SelectListItem> list = cat.Select(x => new SelectListItem { Text = x });
            return View(new SellModel
            {
                MinPrice = 1,
                Categories = categoriesRepository.Categories.Select(x => new Categories { Id = x.CategoryId, Name = x.CategoryName })
            });
        }
        /// <summary>
        /// Sell post action
        /// </summary>
        /// <param name="model">Model to sell</param>
        /// <param name="categ">Lot category</param>
        /// <returns>New lot page</returns>
        [HttpPost]
        public ActionResult Sell(SellModel model, string categ)
        {
            //var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (!ModelState.IsValid)
            {
                model.Categories =
                    categoriesRepository.Categories.Select(
                        x => new Categories {Id = x.CategoryId, Name = x.CategoryName});
                
                return View(model);
            }
            if (categ == null)
            {
                ModelState.AddModelError("",Resources.SellerControllerCategory);
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
                    lot.Images.Add(new Image
                    {
                        ImageMimeType = img.ContentType,
                        ImageData = new byte[img.ContentLength],
                    });
                    img.InputStream.Read(lot.Images[i++].ImageData, 0, img.ContentLength);
                }
            }
            var categoryId = Convert.ToInt32(categ);
            var cat = categoriesRepository.Categories.FirstOrDefault(x => x.CategoryId == categoryId);
            lot.Category = cat;
            lotsRepository.Add(lot);
            return RedirectToAction("Lot", "Lots", new { lotId = lot.LotID });
        }
    }
}