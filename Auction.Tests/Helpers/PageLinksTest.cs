using System;
using System.Web.Mvc;
using Auction.HtmlHelpers;
using Auction.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Auction.Tests.Helpers
{
    [TestClass]
    public class PageLinksTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            HtmlHelper myHelper = null;

            // Arrange 
            PageModel pagingInfo = new PageModel
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Assert
            Assert.AreEqual(result.ToString(), @"<li><a href=""Page1"">1</a></li><li class=""active""><a href=""Page2"">2</a></li><li><a href=""Page3"">3</a></li>");
        }

    }

}
