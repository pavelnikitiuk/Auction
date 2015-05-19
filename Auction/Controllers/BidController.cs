using System;
using System.Linq;
using System.Web.Mvc;
using Auction.Domain.Abstract;
using Auction.Domain.Entities;
using Auction.Models;
using Microsoft.AspNet.Identity;

namespace Auction.Controllers
{
    [Authorize]
    public class BidController : Controller
    {
        private ILotsRepository lotsRepository;
        public BidController(ILotsRepository lotsRepository)
        {
            this.lotsRepository = lotsRepository;
        }
        [HttpPost]
        [Authorize]
        public ActionResult Add(LotModel model)
        {
            Lot lot = lotsRepository.Lots.FirstOrDefault(p => p.LotID == model.Lot.LotID);
                if (lot != null)
                {
                    if (ModelState.IsValid)
                    {
                        {
                            lotsRepository.AddBid(lot, model.BidAmount, User.Identity.GetUserId());
                            lot.CurrentPrice = model.BidAmount;
                            return PartialView("LotPartial", new LotModel() {Lot = lot,NumOnPage = model.NumOnPage});
                        }
                    }
                 
                }
            return PartialView("LotPartial", new LotModel() { Lot = lot,NumOnPage = model.NumOnPage});
        }
    }
}