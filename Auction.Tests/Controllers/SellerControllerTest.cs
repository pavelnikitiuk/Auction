using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            SellModel q = new SellModel {Name = "P1", Files = new List<HttpPostedFileBase>()};
            //Act
            var context = new ValidationContext(q, null, null);
            var results = new List<ValidationResult>();
            //Assert
            var isModelStateValid = Validator.TryValidateObject(q, context, results, true);
            Assert.IsFalse(isModelStateValid);
        }
        [TestMethod]
        public void Sell_ModelStateError_Test()
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
            SellerController controller = new SellerController(null, category.Object);
            controller.ModelState.AddModelError("test", "test");
            SellModel q = new SellModel { Name = "P1", Files = new List<HttpPostedFileBase>() };
            //Act
            ActionResult result = controller.Sell(q,"Cat3");

            // Assert 
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            
        }
        [TestMethod]
        public void Sell_ModelStateValid_Test()
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
            Mock<ILotsRepository> mock = new Mock<ILotsRepository>();
            mock.Setup(m => m.Lots).Returns(new[]
            {
                new Lot {LotID = 1, Name = "P1", IsCompleted = false},
                new Lot {LotID = 2, Name = "P2", IsCompleted = false},
                new Lot {LotID = 3, Name = "P3", IsCompleted = false},
                new Lot {LotID = 4, Name = "P4", IsCompleted = false},
            }.AsQueryable());
            SellerController controller = new SellerController(mock.Object, category.Object);
            controller.ModelState.Clear();
            SellModel q = new SellModel { Name = "P1", Files = new List<HttpPostedFileBase>(),Description = "1"};
            //Act
            ActionResult result = controller.Sell(q, "Cat3");

            // Assert 
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            RedirectToRouteResult routeResult = result as RedirectToRouteResult;
            Assert.AreEqual(routeResult.RouteValues["action"], "Lot");

        }
    }
}
