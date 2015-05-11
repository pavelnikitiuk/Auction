
using System;

namespace Auction.Domain.Entities
{
    public class Lot
    {
        public int LotID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal MinPrice { get; set; }
        public string Category { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsCompleted { get; set; }
    }
}
