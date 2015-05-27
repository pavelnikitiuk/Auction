using System;
using System.Linq;
using Auction.Domain.Abstract;
using Auction.Domain.Entities;
using System.Runtime.InteropServices;

namespace Auction.Domain.DBase
{
    public class BidRepository:IBidsRepository,IDisposable
    {
        private AuctionDbContext context = new AuctionDbContext();
        private bool disposed = false;
        public IQueryable<Bid> Bids
        {
            get { return context.Bids; }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposed)
                return;
            if (disposing)
                context.Dispose();
            disposed = true;
        }

        ~BidRepository()
        {
            Dispose(false);
        }
    }
}
