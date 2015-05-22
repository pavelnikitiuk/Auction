using System.Data.Entity;
using Auction.Domain.Entities;

namespace Auction.Domain.DBase
{
    public class AuctionDbContext:DbContext
    {
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Bid> Bids { get; set; }
       // public DbSet<Comment> Comments  { get; set; }
        public DbSet<Category> Categoryes { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
