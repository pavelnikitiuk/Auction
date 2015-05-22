using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Auction.Domain.Entities
{
    public class Lot
    {
        [ScaffoldColumn(false)]
        [Key]
        public int LotID { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string Description { get; set; }
        [Display(Name = "Start Price")]
        public decimal MinPrice { get; set; }
        [ScaffoldColumn(false)]
        

        [Display(Name = "End of Lot")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy hh:mm}", ApplyFormatInEditMode = true)]

        public DateTime EndTime { get; set; }

        [ScaffoldColumn(false)]
        public bool? IsCompleted { get; set; }
        [ScaffoldColumn(false)]
        public decimal? CurrentPrice { get; set; }

        
        [HiddenInput(DisplayValue = false)]

        public virtual ICollection<Bid> Bids { get; set; }
        [HiddenInput(DisplayValue = false)]
        public virtual List<Image> Images { get; set; }
        [HiddenInput(DisplayValue = false)]
        public virtual Category Category { get; set; }
    }
}
