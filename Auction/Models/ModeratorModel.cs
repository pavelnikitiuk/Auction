using System.Collections.Generic;

namespace Auction.Models
{
    public class ModeratorModel
    {
        public IEnumerable<string> Categories { get; set; }
        public IEnumerable<UsersModel> Users { get; set; } 
    }
}