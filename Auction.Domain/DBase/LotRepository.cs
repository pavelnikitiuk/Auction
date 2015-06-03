using System;
using System.Linq;
using Auction.Domain.Abstract;
using Auction.Domain.Entities;

namespace Auction.Domain.DBase
{
    public class LotRepository : BaseRepository, ILotsRepository
    {
        
        public void Remove(Lot lot)
        {
            Lot entryLotInDb = Context.Lots.Find(lot.LotID);
            if (entryLotInDb != null)
            {
                Context.Lots.Remove(entryLotInDb);
                Context.SaveChanges();
            }
        }

        public void AddBid(Lot lot, decimal bidAmount, string userId)
        {
            var entryLotInDb = Context.Lots.Find(lot.LotID);
            if (entryLotInDb == null)
                return;
            entryLotInDb.Bids.Add(new Bid { BidAmount = bidAmount, DatePlaced = DateTime.Now, Lot = lot, UserId = userId });
            entryLotInDb.CurrentPrice = bidAmount;
            Context.SaveChanges();
        }

        public void Edit(Lot lot, Category category)
        {
            var entryLotInDb = Context.Lots.Find(lot.LotID);
            if (entryLotInDb == null)
                return;
            var dbCategory = Context.Categoryes.Find(category.CategoryId);
            if (dbCategory == null)
                return;
            entryLotInDb.Category.Lots.Remove(entryLotInDb);
            dbCategory.Lots.Add(lot);
            Context.SaveChanges();
        }
        public void Add(Lot lot)
        {
            var addCategory = Context.Categoryes.Find(lot.Category.CategoryId);
            lot.Category = addCategory;
            Context.Lots.Add(lot);
            Context.SaveChanges();
        }
        public IQueryable<Lot> Lots
        {
            get { return Context.Lots; }
        }
    }
}
