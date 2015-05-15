using System;
using System.Linq;
using System.Web.Mvc;
using Auction.Domain.Abstract;
using Auction.Domain.Entities;

namespace Auction.Domain.DBase
{
    public class LotRepository : ILotsRepository
    {


        private AuctionDbContext context = new AuctionDbContext();

        public void AddLot(Lot lot)
        {
            context.Lots.Add(lot);
            context.SaveChanges();
        }
        public void AddBid(Lot lot, decimal bidAmount, string userId)
        {
            Lot db = context.Lots.Find(lot.LotID);
            if (db != null)
            {
                db.Bids.Add(new Bid() {BidAmount = bidAmount, DatePlaced = DateTime.Now, Lot = lot,UserId = userId});
                db.CurrentPrice = bidAmount;
            }

            context.SaveChanges();
        }

        public void Save(Lot lot)
        {
            if (lot.LotID == 0)
            {
                context.Lots.Add(lot);
            }
            else
            {
                Lot db = context.Lots.Find(lot.LotID);
                if (db != null)
                    db.Bids.Add(new Bid(){BidAmount = 10,DatePlaced = DateTime.Now, Lot = lot});
            }
            context.SaveChanges();
        }
        public void EndLot(Lot lot)
        {
            if (lot.LotID == 0)
            {
                context.Lots.Add(lot);
            }
            else
            {
                Lot db = context.Lots.Find(lot.LotID);
                if (db != null)
                    db.IsCompleted = true;
            }
            context.SaveChanges();
        }
        public IQueryable<Lot> Lots
        {
            get { return context.Lots; }
        }
    }
}
