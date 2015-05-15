using System.Data.Entity;
using Auction.Domain.Entities;

namespace Auction.Domain.DBase
{
    public class AuctionDbContext:DbContext
    {
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Bid> Bids { get; set; }
    }
}
