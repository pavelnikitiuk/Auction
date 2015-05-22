using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Auction.Domain.Entities;

namespace Auction.Domain.Abstract
{
    public interface ICategoriesRepository
    {
        void AddLot(Category category,Lot lot);
        IQueryable<Category> Categories { get; }
    }
}
