using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Auction.Domain.Abstract;
using Auction.Domain.Entities;
using Auction.Models;
using Auction.Properties;

namespace Auction.Controllers
{
    public class LotsController : Controller
    {
        private ILotsRepository lotsRepository;
        private ICategoriesRepository categoriesRepository;
        public int PageSize = 5;
        public LotsController(ILotsRepository lotsRepository, ICategoriesRepository categoriesRepository)
        {
            this.lotsRepository = lotsRepository;
            this.categoriesRepository = categoriesRepository;
        }
        /// <summary>
        /// Search lot
        /// </summary>
        /// <param name="search">Name of lot</param>
        /// <param name="page">Current page</param>
        /// <returns>ViewResult of finding lot</returns>
        [HttpGet]
        public ActionResult SearchLot(string search, int page = 1)
        {
            if (search == null)
                search = "";
            if (!ModelState.IsValid)
                return RedirectToAction("List", "Lots");
            var allLots = lotsRepository.Lots.Where(p => p.Name.Contains(search) && p.IsCompleted == false);
            var count = allLots.Count();
            var lots = allLots.OrderBy(p => p.LotID)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize);

            LotsListViewModel model = new LotsListViewModel
            {
                Lots = lots,
                PageModel = new PageModel
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = count
                },
                CurrentCategoryName = search,
            };
            return View(model);
        }

        /// <summary>
        /// Lot page
        /// </summary>
        /// <param name="lotId">Lot Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Lot(int? lotId)
        {
            if (lotId == null)
                return RedirectToAction("List");
            var lot = lotsRepository.Lots.FirstOrDefault(x => x.LotID == lotId);
            if (lot != null)
                return View(lot);
            return RedirectToAction("List");
        }

        /// <summary>
        /// Remove lot
        /// </summary>
        /// <param name="lotId">Remove lot id</param>
        /// <param name="url">Return url</param>
        /// <returns>return to previous url</returns>
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
        /// <summary>
        /// Edit get actio
        /// </summary>
        /// <param name="lotId">Lot id to edit</param>
        /// <returns>View to edit lot</returns>
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
                    Categories = categoriesRepository.Categories.Select(x => new Categories { Id = x.CategoryId, Name = x.CategoryName }).ToList(),
                    Description = prod.Description,
                    LotID = prod.LotID,
                    Name = prod.Name
                });
            return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// Edit post action
        /// </summary>
        /// <param name="model">Edit model</param>
        /// <param name="categId">new category</param>
        /// <returns>Edit Lot page</returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "moderator")]
        public ActionResult Edit(EditModel model, string categId)
        {
            int categoryId = Convert.ToInt32(categId);
            if (!ModelState.IsValid)
            {
                model.Categories =
                    categoriesRepository.Categories.Select(
                        x => new Categories { Id = x.CategoryId, Name = x.CategoryName }).ToList();
                return View(model);
            }

            Lot prod = lotsRepository.Lots.FirstOrDefault(p => p.LotID == model.LotID);

            if (prod == null)
                return View(model);

            prod.Name = model.Name;
            prod.Description = model.Description;

            var cat = categoriesRepository.Categories.FirstOrDefault(x => x.CategoryId == categoryId);
            if (cat == null)
            {
                ModelState.AddModelError("", Resources.LotsControllerUnknownCategory);
                return View(model);
            }
            lotsRepository.Edit(prod, cat);
            return RedirectToAction("Lot", "Lots", new { lotId = model.LotID });

        }
        /// <summary>
        /// Get Image by Id
        /// </summary>
        /// <param name="lotId">Lot id</param>
        /// <param name="num">number of images</param>
        /// <returns>Lot image</returns>
        public FileContentResult GetImage(int lotId, int num)
        {
            Lot prod = lotsRepository.Lots.FirstOrDefault(p => p.LotID == lotId);
            if (prod != null)
            {
                if (prod.Images != null)
                    if (prod.Images.Any())
                    {
                        var image = prod.Images[num];
                        return File(image.ImageData, image.ImageMimeType);
                    }
                return File(System.IO.File.ReadAllBytes(HttpContext.Server.MapPath(Resources.DefaultImage)), Resources.DefaultImageType);
            }
            return null;
        }
        /// <summary>
        /// List of Lots
        /// </summary>
        /// <param name="categoryId">Lot category</param>
        /// <param name="page">Number of page</param>
        /// <returns>List lot page</returns>
        [HttpGet]
        public ViewResult List(int? categoryId, int page = 1)
        {
            var selectCategory = categoriesRepository.Categories.FirstOrDefault(x => x.CategoryId == categoryId);
            IEnumerable<Lot> categoryLots;
            if (selectCategory == null)
                categoryLots = lotsRepository.Lots;
            else
                categoryLots = selectCategory.Lots;
            LotsListViewModel model = new LotsListViewModel
            {
                Lots = categoryLots.OrderBy(p => p.LotID)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PageModel = new PageModel
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = categoryLots.Count()
                },
                CurrentCategoryName = selectCategory == null ? "All" : selectCategory.CategoryName,
                CurrentCategoryId = selectCategory == null ? null : (int?)selectCategory.CategoryId
            };
            return View(model);
        }
    }
}