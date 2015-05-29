using System;
using System.Text;
using Auction.Models;
using System.Web.Mvc;

namespace Auction.HtmlHelpers
{
    public static class PagingHelper
    {
        /// <summary>
        /// Create page links
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="pageModel">Model of page</param>
        /// <param name="pageUrl">links Url</param>
        /// <returns>Page links</returns>
        public static MvcHtmlString PageLinks(this HtmlHelper html, PageModel pageModel, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageModel.TotalPages; i++)
            {
                TagBuilder li = new TagBuilder("li");
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href",pageUrl(i));
                tag.InnerHtml = i.ToString();
                if(i == pageModel.CurrentPage)
                    li.AddCssClass("active");
                li.InnerHtml = tag.ToString();
                result.Append(li);
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}