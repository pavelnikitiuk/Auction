using System.Web.Mvc;

namespace Auction.HtmlHelpers
{
    public static class UserRole
    {
        /// <summary>
        /// Check user role
        /// </summary>
        /// <param name="value"></param>
        /// <param name="evaluation">bool expression</param>
        /// <returns></returns>
        public static MvcHtmlString IsRole(this MvcHtmlString value, bool evaluation)
        {
            return evaluation?value:MvcHtmlString.Empty;
            
        }

        public static UrlHelper IsRole(this UrlHelper value, bool evaluation)
        {
            return evaluation ? value : MvcHtmlString.Empty;

        }
    }
}