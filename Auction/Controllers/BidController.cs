using System;
using System.Linq;
using System.Web.Mvc;
using Auction.Anatation;
using Auction.Domain.Abstract;
using Auction.Domain.Entities;
using Auction.Models;
using Auction.Properties;
using Microsoft.AspNet.Identity;

namespace Auction.Controllers
{
    public class BidController : Controller
    {
        private ILotsRepository lotsRepository;
        public BidController(ILotsRepository lotsRepository)
        {
            this.lotsRepository = lotsRepository;
        }
        /// <summary>
        /// Add new Bid
        /// </summary>
        /// <param name="model">Lot View model</param>
        /// <returns>ActionResult of new Bid</returns>
        [HttpPost]
        [AjaxAuthorize]
        public ActionResult Add(LotModel model)
        {
            Lot lot = lotsRepository.Lots.FirstOrDefault(p => p.LotID == model.Lot.LotID);
            if (lot != null)
            {
                if (ModelState.IsValid)
                {
                    if (DateTime.Now >= lot.EndTime)
                        ModelState.AddModelError("", Resources.BidControllerEnd);
                    else
                    {
                        lotsRepository.AddBid(lot, model.BidAmount, User.Identity.GetUserId());
                        lot.CurrentPrice = model.BidAmount;
                    }
                }
            }
            return PartialView("LotPartial", new LotModel() { Lot = lot, NumOnPage = model.NumOnPage });
        }
    }
}