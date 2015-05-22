using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auction.Models
{
    public class RolesModel
    {
        public List<SelectListItem> Roles { get; set; }
        public IList<string> RolesForThisUser { get; set; }
        
    }
}