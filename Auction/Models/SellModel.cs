using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Auction.Anatation;

namespace Auction.Models
{
    public class SellModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string Description { get; set; }
        [Display(Name = "Start Price")]
        [Range(0.1, Double.MaxValue)]
        public decimal MinPrice { get; set; }
        [ScaffoldColumn(false)]


        [Display(Name = "End of Lot")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [EndTimeAtribute]
        public DateTime EndTime { get; set; }

        public IEnumerable<Categories> Categories { get; set; }
        [FileSizeAttribute(10000000, ErrorMessage = "Maximum file size should not exceed 10MB")]

        public IEnumerable<HttpPostedFileBase> Files { get; set; }
    }
}