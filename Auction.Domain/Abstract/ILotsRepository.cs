using System.Linq;
using Auction.Domain.Entities;

namespace Auction.Domain.Abstract
{
    public interface ILotsRepository
    {
        void EndLot(Lot lot);
        IQueryable<Lot> Lots { get; }
    }
}