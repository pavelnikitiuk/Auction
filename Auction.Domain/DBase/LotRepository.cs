using System.Linq;
using Auction.Domain.Abstract;
using Auction.Domain.Entities;

namespace Auction.Domain.DBase
{
    public class LotRepository:ILotsRepository
    {


        private AuctionDbContext context = new AuctionDbContext();

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
