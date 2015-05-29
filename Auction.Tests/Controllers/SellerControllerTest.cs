using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Auction.Controllers;
using Auction.Domain.Abstract;
using Auction.Domain.Entities;
using Auction.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using Moq;

namespace Auction.Tests.Controllers
{
    [TestClass]
    public class SellerControllerTest
    {
        [TestMethod]
        public void Sell_Test()
        {
            // Arrange
            Mock<ICategoriesRepository> category = new Mock<ICategoriesRepository>();
            category.Setup(m => m.Categories).Returns(new[]
            {
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Cat1",
                },
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Cat2",
                },
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Cat3",
                }
            }.AsQueryable());
            SellerController target = new SellerController(null, category.Object);
            target.Sell();
        }
        [TestMethod]
        public void Sell_Post_Validation_Erorr_Test()
        {
            //Arrange
            Mock<ICategoriesRepository> category = new Mock<ICategoriesRepository>();
            category.Setup(m => m.Categories).Returns(new[]
            {
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Cat1",
                },
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Cat2",
                },
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Cat3",
                }
            }.AsQueryable());
            SellModel q =  new SellModel {Name = "P1"};
            q.Files = new List<HttpPostedFileBase>();
                //Act
                SellerController target = new SellerController(null, category.Object);
            var result1 = target.Sell(q, null);
            //Assert
            Assert.IsInstanceOfType(result1, typeof(RedirectToRouteResult));
            RedirectToRouteResult routeResult1 = result1 as RedirectToRouteResult;
            Assert.AreEqual(routeResult1.RouteValues["action"], "Lot");
        }
    }
}
