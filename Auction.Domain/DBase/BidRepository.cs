using System;
using System.Collections.Generic;
using System.Linq;
using Auction.Domain.Abstract;
using Auction.Domain.Entities;

namespace Auction.Domain.DBase
{
    public class BidRepository:IBidsRepository
    {
        private AuctionDbContext context = new AuctionDbContext();
        public IQueryable<Bid> Bids
        {
            get { return context.Bids; }
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
