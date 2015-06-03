using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auction.Domain.Abstract;
using Auction.Domain.Entities;

namespace Auction.Domain.DBase
{

    public class CategoryRepository : BaseRepository, ICategoriesRepository
    {
        public void Remove(Category category)
        {
            var entryCategoryInDb = Context.Categoryes.Find(category.CategoryId);
            if (entryCategoryInDb == null)
                return;
            Context.Categoryes.Remove(entryCategoryInDb);
            Context.SaveChanges();
        }
        public void Add(string name)
        {
            Context.Categoryes.Add(new Category { CategoryName = name });
            Context.SaveChanges();
        }
        public IQueryable<Category> Categories { get { return Context.Categoryes; } }
        
    }
}
