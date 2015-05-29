using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Auction.Domain.Entities;
using Auction.Anatation;

namespace Auction.Models
{
    public class LotModel
    {
        public int NumOnPage { get; set; }
        public Lot Lot { get; set; }
        [DisplayName("Bid Amount")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [CurrentPriceAtribute]
        public decimal BidAmount { get; set; }
    }
}