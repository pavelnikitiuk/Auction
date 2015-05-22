using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auction.Domain.Abstract;
using Auction.Domain.Entities;

namespace Auction.Domain.DBase
{

    public class CategoryRepository : ICategoriesRepository
    {
        private AuctionDbContext context = new AuctionDbContext();

        public void AddLot(Category category, Lot lot)
        {
            Category db = context.Categoryes.Find(category.CategoryId);
            if (db != null)
            {
                db.Lots.Add(lot);
                context.SaveChanges();
            }
        }
        public IQueryable<Category> Categories { get { return context.Categoryes; } }
    }
}
