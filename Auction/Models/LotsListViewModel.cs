using System.Collections.Generic;
using Auction.Domain.Entities;

namespace Auction.Models
{
    public class LotsListViewModel
    {
        public IEnumerable<Lot> Lots { get; set; }
        public PageModel PageModel { get; set; }
        public string CurrentCategory { get; set; }
    }
}