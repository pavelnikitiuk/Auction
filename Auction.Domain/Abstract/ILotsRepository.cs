using System.Linq;
using Auction.Domain.Entities;

namespace Auction.Domain.Abstract
{
    public interface ILotsRepository
    {
        void Remove(Lot lot);
        void AddBid(Lot lot, decimal bidAmount, string userId);
        void Save(Lot lot);
        void Edit(Lot lot, Category category);
        IQueryable<Lot> Lots { get; }
    }
}