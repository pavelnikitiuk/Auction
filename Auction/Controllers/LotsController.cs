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
        private ILotsRepository repository;
        private IBidsRepository bidsRepository;
        public int PageSize = 4;
        public LotsController(ILotsRepository productRepository, IBidsRepository bidsRepository)
        {
            repository = productRepository;
            this.bidsRepository = bidsRepository;
         //   this.bidsRepository.Save();
        }
        [HttpPost]
        public ActionResult SearchResult(string name, int page = 1)
        {
            var lots =
                repository.Lots.Where(
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
                        repository.Lots.Count() :
                        lots.Count()
                },

            };
            return View(model);

        }
        [Authorize(Roles = "admin")]
        public ActionResult Remove(int lotId, string url)
        {
            Lot lot = repository.Lots.FirstOrDefault(p => p.LotID == lotId);
            if (lot != null)
            {
                if (ModelState.IsValid)
                    repository.Remove(lot);

            }
            if (url != null)
                return Redirect(url);
            return RedirectToAction("List", "Lots");
        }
        public FileContentResult GetImage(int lotId)
        {
            Lot prod = repository.Lots.FirstOrDefault(p => p.LotID == lotId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            return null;
        }
        public ViewResult List(string category, int page = 1)
        {
            LotsListViewModel model = new LotsListViewModel
            {
                Lots = repository.Lots.Where(p => category == null || p.Category == category).OrderBy(p => p.LotID).Skip((page - 1) * PageSize).Take(PageSize),
                PageModel = new PageModel
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                        repository.Lots.Count() :
                        repository.Lots.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }
    }
}