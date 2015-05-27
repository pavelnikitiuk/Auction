using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Auction.Anatation;

namespace Auction.Domain.Entities
{
    public class Lot
    {
        [Key]
        public int LotID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Start Price")]
        public decimal MinPrice { get; set; }
        

        
        public DateTime EndTime { get; set; }

        public bool IsCompleted { get; set; }
        public decimal CurrentPrice { get; set; }

        

        public virtual ICollection<Bid> Bids { get; set; }
        public virtual List<Image> Images { get; set; }
        public virtual Category Category { get; set; }
    }
}
