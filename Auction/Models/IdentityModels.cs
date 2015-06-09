﻿using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Auction.Models
{
    
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? IsSeller { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("AuctionUserDb")
        {
            Database.SetInitializer<ApplicationDbContext>(new AppDbInitializer());
        }
    }
}