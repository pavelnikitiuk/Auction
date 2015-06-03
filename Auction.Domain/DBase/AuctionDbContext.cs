using System.Data.Entity;
using Auction.Domain.Entities;

namespace Auction.Domain.DBase
{
    public class AuctionDbContext : DbContext
    {
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Bid> Bids { get; set; }
        // public DbSet<Comment> Comments  { get; set; }
        public DbSet<Category> Categoryes { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lot>().HasMany(x => x.Bids).WithRequired(x => x.Lot).WillCascadeOnDelete();
            modelBuilder.Entity<Lot>().HasMany(x => x.Images).WithRequired(x => x.Lot).WillCascadeOnDelete();
            modelBuilder.Entity<Category>().HasMany(x => x.Lots).WithRequired(x => x.Category).WillCascadeOnDelete();
        }
    }
}
