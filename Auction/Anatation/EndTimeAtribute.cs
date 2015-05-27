using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Auction.Domain.Entities;

namespace Auction.Anatation
{
    public class EndTimeAtribute : ValidationAttribute
    {
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            if ((DateTime) value >= DateTime.Now.AddHours(1))
                return ValidationResult.Success;
            return new ValidationResult("The minimum time of the auction shall be one hour");
        }
    }
}