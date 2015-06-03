using System.Collections.Generic;
using Auction.Domain.Entities;

namespace Auction.Models
{
    public class LotsListViewModel
    {
        public IEnumerable<Lot> Lots { get; set; }
        public PageModel PageModel { get; set; }
        public int? CurrentCategoryId { get; set; }
        public string CurrentCategoryName { get; set; }
    }
}