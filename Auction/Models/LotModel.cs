using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Auction.Domain.Entities;
using Auction.Anatation;

namespace Auction.Models
{
    public class LotModel
    {
        public Lot Lot { get; set; }
        [DisplayName("Bid Amount")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [CurrentPriceAtribute]
        public decimal BidAmount { get; set; }
    }
}