using System;
using System.ComponentModel.DataAnnotations;

namespace Auction.Anatation
{
    public class EndTimeAtribute : ValidationAttribute
    {
        /// <summary>
        /// Validate time on sell lot
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns>ValidationResult, can sell</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
           if (value != null)
                if ((DateTime)value >= DateTime.Now.AddHours(1))
                    return ValidationResult.Success;
           return new ValidationResult("The minimum time of the auction shall be one hour");
        }
    }
}