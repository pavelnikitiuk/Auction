using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Auction.Domain.Entities
{
    /// <summary>
    /// Category of Lots
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Id of Category
        /// </summary>
        [Key]
        public int CategoryId { get; set; }
        /// <summary>
        /// Name of category
        /// </summary>
        [Required(ErrorMessage = "Category is required.")]
        [StringLength(40, ErrorMessage = "Category cannot be longer than 40 characters.")]
        public string CategoryName { get; set; }
        /// <summary>
        /// Lots in this category
        /// </summary>
        public virtual ICollection<Lot> Lots { get; set; }
    }
}
