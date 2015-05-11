using System.Data.Entity;
using Auction.Domain.Entities;

namespace Auction.Domain.DBase
{
    class AuctionDbContext:DbContext
    {
        public DbSet<Lot> Lots { get; set; }
    }
}
