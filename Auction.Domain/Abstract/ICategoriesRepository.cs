using System.Linq;
using Auction.Domain.Entities;

namespace Auction.Domain.Abstract
{
    public interface ICategoriesRepository
    {
        void AddLot(Category category,Lot lot);
        IQueryable<Category> Categories { get; }
    }
}
