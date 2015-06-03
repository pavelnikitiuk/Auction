using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Auction.Models
{
    public class EditModel
    {
        [HiddenInput(DisplayValue = false)]
        public int LotID { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string Description { get; set; }

        public List<Categories> Categories { get; set; }
    }

    public class Categories
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}