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
        public RedirectToRouteResult Add(LotModel model)
        {
            if (ModelState.IsValid)
            {
                Lot lot = lotsRepository.Lots.FirstOrDefault(p => p.LotID == model.Lot.LotID);
                if (lot != null)
                {
                    lotsRepository.AddBid(lot, model.BidAmount,User.Identity.GetUserId());
                }
                return RedirectToAction("List", "Lots");
            }
            return RedirectToAction("List", "Lots",model);
            
        }
    }
}