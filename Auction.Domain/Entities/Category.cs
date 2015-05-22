using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Domain.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        [StringLength(40, ErrorMessage = "Category cannot be longer than 40 characters.")]
        public string CategoryName { get; set; }
        public virtual ICollection<Lot> Lots { get; set; }
    }
}
