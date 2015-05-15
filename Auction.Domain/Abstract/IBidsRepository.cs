using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Auction.Domain.Entities;

namespace Auction.Domain.Abstract
{
    public interface IBidsRepository
    {
       IQueryable<Bid> Bids { get;}
        void Save();
    }
}