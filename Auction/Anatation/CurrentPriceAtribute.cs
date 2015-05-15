using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Auction.Domain.Abstract;
using Auction.Domain.Entities;

namespace Auction.Anatation
{
    public class CurrentPriceAtribute:ValidationAttribute
    {
        
        protected override ValidationResult IsValid(object value,ValidationContext validationContext)
        {
            var target = validationContext.ObjectType.GetProperty("Lot");
            var targetValue = (Lot)target.GetValue(validationContext.ObjectInstance, null);
            decimal val;
            
            if (targetValue != null)
            {
                if ((decimal) value > targetValue.CurrentPrice + (decimal) 0.1)
                {
                    return ValidationResult.Success;
                }
                {
                    return new ValidationResult("Bit mus be greather then current price");
                }
            }
            return new ValidationResult("Bit mus be greather then current price");
        }
    }
}