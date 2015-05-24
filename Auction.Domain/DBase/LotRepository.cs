using System;
using System.Linq;
using Auction.Domain.Abstract;
using Auction.Domain.Entities;

namespace Auction.Domain.DBase
{
    public class LotRepository : ILotsRepository
    {
        private readonly AuctionDbContext context = new AuctionDbContext();
        public void Remove(Lot lot)
        {
            Lot dbEntry = context.Lots.Find(lot.LotID);
            if (dbEntry != null)
            {
                context.Images.RemoveRange(dbEntry.Images);
                context.Lots.Remove(dbEntry);
                context.SaveChanges();
            }
        }

        public void AddBid(Lot lot, decimal bidAmount, string userId)
        {
            var db = context.Lots.Find(lot.LotID);
            if (db == null)
                return;
            db.Bids.Add(new Bid() { BidAmount = bidAmount, DatePlaced = DateTime.Now, Lot = lot, UserId = userId });
            db.CurrentPrice = bidAmount;
            context.SaveChanges();
        }

        public void Edit(Lot lot, Category category)
        {
            var dbLot = context.Lots.Find(lot.LotID);
            if (dbLot == null)
                return;
            var dbCategory = context.Categoryes.Find(category.CategoryId);
            if (dbCategory == null)
                return;
            dbLot.Category.Lots.Remove(dbLot);
            dbCategory.Lots.Add(lot);
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
                    db.Bids.Add(new Bid { BidAmount = 10, DatePlaced = DateTime.Now, Lot = lot });
            }
            context.SaveChanges();
        }
        public IQueryable<Lot> Lots
        {
            get { return context.Lots; }
        }
    }
}
