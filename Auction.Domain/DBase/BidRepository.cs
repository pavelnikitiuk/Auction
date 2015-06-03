using System;
using System.Linq;
using Auction.Domain.Abstract;
using Auction.Domain.Entities;
using System.Runtime.InteropServices;

namespace Auction.Domain.DBase
{
    public class BidRepository : BaseRepository, IBidsRepository
    {
        public IQueryable<Bid> Bids
        {
            get { return Context.Bids; }
        }
    }
}
