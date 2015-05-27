using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auction.Models
{
    public class ModeratorModel
    {
        public IEnumerable<string> Categories { get; set; }
        public IEnumerable<string> Users { get; set; } 
    }
}