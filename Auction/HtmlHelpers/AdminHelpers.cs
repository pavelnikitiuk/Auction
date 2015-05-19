using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Auction.Models;

namespace Auction.HtmlHelpers
{
    public static class AdminHelpers
    {
        public static MvcHtmlString RemoveHelper(this MvcHtmlString value, bool evaluation)
        {
            return evaluation?value:MvcHtmlString.Empty;
            
        }
    }
}