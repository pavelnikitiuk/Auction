using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Auction.Domain.Entities;

namespace Auction.Models
{
    public class SellModel
    {
        public Lot Lot { get; set; }
        public IEnumerable<string> Categories { get; set; }
    }
}