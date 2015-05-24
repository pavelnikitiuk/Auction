using System.Linq;
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
        public int PageSize = 4;
        public LotsController(ILotsRepository lotsRepository, ICategoriesRepository categoriesRepository)
        {
            this.lotsRepository = lotsRepository;
            this.categoriesRepository = categoriesRepository;
        }

        [HttpGet]
        public ActionResult Lot(int? lotId)
        {
            if (lotId == null)
                return RedirectToAction("List");
            var lot = lotsRepository.Lots.FirstOrDefault(x => x.LotID == lotId);
            if (lot != null) 
                return View(lot);
            return View();
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
        [Authorize(Roles = "moderator")]
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
        [HttpGet]
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "moderator")]
        public ActionResult Edit(int? lotId)
        {
            if (lotId == null)
                return RedirectToAction("Index", "Home");
            Lot prod = lotsRepository.Lots.FirstOrDefault(p => p.LotID == lotId);
            if (prod != null)
                return View(new EditModel
                {
                    Categories = categoriesRepository.Categories.Select(x => x.CategoryName).OrderBy(x => x),
                    Description = prod.Description,
                    LotID = prod.LotID,
                    Name = prod.Name
                });
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "moderator")]
        public ActionResult Edit(EditModel model, string categ)
        {
            if (!ModelState.IsValid)
                return View(model);

            Lot prod = lotsRepository.Lots.FirstOrDefault(p => p.LotID == model.LotID);

            if (prod == null)
                return View(model);

            prod.Name = model.Name;
            prod.Description = model.Description;

            var cat = categoriesRepository.Categories.FirstOrDefault(x => x.CategoryName == categ);
            if (cat == null)
            {
                ModelState.AddModelError("","Unknown category");
                return View(model);
            }
            lotsRepository.Edit(prod,cat);
            return RedirectToAction("Lot", "Lots", new { lotId = model.LotID });

        }
        public FileContentResult GetImage(int lotId, int num)
        {
            Lot prod = lotsRepository.Lots.FirstOrDefault(p => p.LotID == lotId);
            if (prod != null)
            {
                if (prod.Images.Any())
                {
                    var image = prod.Images[num];
                    return File(image.ImageData, image.ImageMimeType);
                }
                return File(System.IO.File.ReadAllBytes(HttpContext.Server.MapPath(Properties.Resources.DefaultImage)), Properties.Resources.DefaultImageType);
            }
            return null;
        }

        [HttpGet]
        public ViewResult List(string category, int page = 1)
        {
            LotsListViewModel model = new LotsListViewModel
            {
                Lots = lotsRepository.Lots.Where(p => (category == null || p.Category.CategoryName == category)&&p.IsCompleted==false ).OrderBy(p => p.LotID).Skip((page - 1) * PageSize).Take(PageSize),
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