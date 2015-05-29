using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Auction.Domain.Entities
{
    /// <summary>
    /// Images of lot
    /// </summary>
    public class Image
    {
        /// <summary>
        /// Image Id
        /// </summary>
        [Key]
        public int ImageId { get; set; }
        /// <summary>
        /// Data of image
        /// </summary>
        public byte[] ImageData { get; set; }
        /// <summary>
        /// Image type
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
        /// <summary>
        /// Images Lot
        /// </summary>
        public virtual Lot Lot { get; set; }
    }
}
