using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Auction.Anatation;
using Auction.Domain.Entities;

namespace Auction.Models
{
    public class SellModel
    {
        public Lot Lot { get; set; }
        public IEnumerable<string> Categories { get; set; }
        [FileSizeAttribute(10000000, ErrorMessage = "Maximum file size should not exceed 10MB")]

        public IEnumerable<HttpPostedFileBase> Files { get; set; }
    }
}