using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Auction.Domain.Entities
{
    /// <summary>
    /// Lot
    /// </summary>
    public class Lot
    {
        /// <summary>
        /// Lot's Id
        /// </summary>
        [Key]
        public int LotID { get; set; }
        /// <summary>
        /// Lot's name
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// Lot's description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Min price to bid
        /// </summary>
        [Display(Name = "Start Price")]
        public decimal MinPrice { get; set; }
        /// <summary>
        /// Lot's end time
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// Is lot complete
        /// </summary>
        public bool IsCompleted { get; set; }
        /// <summary>
        /// Current price of lot
        /// </summary>
        public decimal CurrentPrice { get; set; }

        
        /// <summary>
        /// Lot's bids
        /// </summary>
        public virtual ICollection<Bid> Bids { get; set; }
        /// <summary>
        /// Lot's images
        /// </summary>
        public virtual List<Image> Images { get; set; }
        /// <summary>
        /// Lot's category
        /// </summary>
        public virtual Category Category { get; set; }

    }
}
