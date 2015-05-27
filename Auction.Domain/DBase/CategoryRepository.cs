using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auction.Domain.Abstract;
using Auction.Domain.Entities;

namespace Auction.Domain.DBase
{

    public class CategoryRepository : ICategoriesRepository, IDisposable
    {
        private bool disposed = false;
        private AuctionDbContext context = new AuctionDbContext();

        public void Remove(Category category)
        {
            var db = context.Categoryes.Find(category.CategoryId);
            if (db == null)
                return;
            db.Lots.Clear();
            context.Categoryes.Remove(db);
            context.SaveChanges();
        }
        public void Add(string name)
        {
            context.Categoryes.Add(new Category { CategoryName = name });
            context.SaveChanges();
        }
        public void AddLot(Category category, Lot lot)
        {
            Category db = context.Categoryes.Find(category.CategoryId);
            if (db != null)
            {
                db.Lots.Add(lot);
                context.SaveChanges();
            }
        }

        public void Edit(Category newCategory, Lot lot)
        {
            Category newcat = context.Categoryes.Find(newCategory.CategoryId);
            if (newcat == null)
                return;
            Category oldcat = context.Categoryes.Find(lot.Category.CategoryId);
            if (oldcat == null)
                return;
            Lot l = context.Lots.Find(lot.LotID);
            if (l == null)
                return;
            oldcat.Lots.Remove(l);
            context.SaveChanges();
            newcat.Lots.Add(lot);
            context.SaveChanges();

        }
        public IQueryable<Category> Categories { get { return context.Categoryes; } }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
                context.Dispose();
            disposed = true;
        }

        ~CategoryRepository()
        {
            Dispose(false);
        }
    }
}
