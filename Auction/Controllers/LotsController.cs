using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.Mvc;
using Auction.Domain.Abstract;
using Auction.Domain.Entities;
using Auction.Models;

namespace Auction.Controllers
{
    public class LotsController : Controller
    {
        private ILotsRepository lotsRepository;
        private ICategoriesRepository categoriesRepository;
        private IBidsRepository bidsRepository;
        public int PageSize = 4;
        public LotsController(ILotsRepository lotsRepository, IBidsRepository bidsRepository, ICategoriesRepository categoriesRepository)
        {
            this.lotsRepository = lotsRepository;
            this.bidsRepository = bidsRepository;
            this.categoriesRepository = categoriesRepository;
        }

        [HttpGet]
        public ActionResult Lot(int lotId)
        {
            var lot = lotsRepository.Lots.FirstOrDefault(x => x.LotID == lotId);
            return View(lot);
        }

        [HttpPost]
        public ActionResult SearchResult(string name, int page = 1)
        {
            var lots =
                lotsRepository.Lots.Where(
                    p =>
                        p.Name == name
                        )
                    .OrderBy(p => p.LotID)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize);
            LotsListViewModel model = new LotsListViewModel
            {
                Lots = lots,
                PageModel = new PageModel
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = name == null ?
                        lotsRepository.Lots.Count() :
                        lots.Count()
                },

            };
            return View(model);

        }
        [Authorize(Roles = "admin")]
        public ActionResult Remove(int lotId, string url)
        {
            Lot lot = lotsRepository.Lots.FirstOrDefault(p => p.LotID == lotId);
            if (lot != null)
            {
                if (ModelState.IsValid)
                    lotsRepository.Remove(lot);

            }
            if (url != null)
                return Redirect(url);
            return RedirectToAction("List", "Lots");
        }
        public FileContentResult GetImage(int lotId, int num)
        {
            Lot prod = lotsRepository.Lots.FirstOrDefault(p => p.LotID == lotId);
            if (prod != null)
            {
                if (prod.Images.First() != null)
                {
                    var image = prod.Images[num];
                    return File(image.ImageData, image.ImageMimeType);
                }
                return File(System.IO.File.ReadAllBytes("~/Content/Image/image.jpg"), Properties.Resources.DefaultImageType);
            }
            return null;
        }
        public ViewResult List(string category, int page = 1)
        {
            LotsListViewModel model = new LotsListViewModel
            {
                Lots = lotsRepository.Lots.Where(p => category == null || p.Category.CategoryName == category).OrderBy(p => p.LotID).Skip((page - 1) * PageSize).Take(PageSize),
                PageModel = new PageModel
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                        lotsRepository.Lots.Count() :
                        lotsRepository.Lots.Count(e => e.Category.CategoryName == category)
                },
                CurrentCategory = category
            };
            return View(model);
        }
    }
}