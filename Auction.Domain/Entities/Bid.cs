using System;
using System.ComponentModel.DataAnnotations;

namespace Auction.Domain.Entities
{
    /// <summary>
    /// Bid
    /// </summary>
    public class Bid
    {
        /// <summary>
        /// Id of Bid
        /// </summary>
        [Key]
        public int BidID { get; set; }
        /// <summary>
        /// Amount of Bid
        /// </summary>
        public decimal BidAmount { get; set; }
        /// <summary>
        /// Time of Bid
        /// </summary>
        public DateTime DatePlaced { get; set; }
        /// <summary>
        /// User, who make Bid
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Lot Id of Bid
        /// </summary>
        public virtual Lot Lot { get; set; }

    }
}