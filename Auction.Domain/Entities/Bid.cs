using System;
using System.ComponentModel.DataAnnotations;
using Auction.Domain.Abstract;

namespace Auction.Domain.Entities
{
    public class Bid
    {
        [Key]
        public int BidID { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime DatePlaced { get; set; }
        public string UserId { get; set; }
        public int LotId { get; set; }
        public virtual Lot Lot { get; set; }

    }
}