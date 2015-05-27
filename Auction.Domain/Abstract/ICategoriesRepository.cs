using System.Linq;
using Auction.Domain.Entities;

namespace Auction.Domain.Abstract
{
    public interface ICategoriesRepository
    {
        void AddLot(Category category,Lot lot);
        void Add(string name);
        void Remove(Category category);
        IQueryable<Category> Categories { get; }
    }
}
