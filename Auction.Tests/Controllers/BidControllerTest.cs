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
using Moq;

namespace Auction.Tests.Controllers
{
    [TestClass]
    public class BidControllerTest
    {
        [TestMethod]
        public void Add_Bid_Test()
        {
            //Arrange
            Mock<ILotsRepository> mock = new Mock<ILotsRepository>();
            mock.Setup(m => m.Lots).Returns(new[] {
            new Lot {LotID = 1, Name = "P1",IsCompleted = false},
            new Lot {LotID = 2, Name = "P2",IsCompleted = false},
            new Lot {LotID = 3, Name = "P3",IsCompleted = false},
            new Lot {LotID = 4, Name = "P4",IsCompleted = false},
          }.AsQueryable());
            var target = new BidController(mock.Object);

            //Act
            var result = target.Add(new LotModel
            {
                BidAmount = 10,
                Lot = mock.Object.Lots.First(),
                NumOnPage = 1
            });
            //Arrange


        }
        [TestMethod]
        public void Bid_ModelStateError_Test()
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
            Mock<ILotsRepository> mock = new Mock<ILotsRepository>();
            mock.Setup(m => m.Lots).Returns(new[]
            {
                new Lot {LotID = 1, Name = "P1", Category = category.Object.Categories.ToList()[0]},
                new Lot {LotID = 2, Name = "P2", Category = category.Object.Categories.ToList()[1]},
                new Lot {LotID = 3, Name = "P3", Category = category.Object.Categories.ToList()[0]},
                new Lot {LotID = 4, Name = "P4", Category = category.Object.Categories.ToList()[1]},
                new Lot {LotID = 5, Name = "P5", Category = category.Object.Categories.ToList()[2]}
            }.AsQueryable());
            BidController controller = new BidController(mock.Object);
            controller.ModelState.AddModelError("test", "test");
            //Act
            ActionResult result = controller.Add(new LotModel()
            {
                BidAmount = 10,
                Lot = mock.Object.Lots.First()
            });

            // Assert 
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));

        }
        [TestMethod]
        public void Bid_ModelStateValid_Test()
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
            Mock<ILotsRepository> mock = new Mock<ILotsRepository>();
            mock.Setup(m => m.Lots).Returns(new[]
            {
                new Lot {LotID = 1, Name = "P1", Category = category.Object.Categories.ToList()[0],EndTime = new DateTime(2016)},
            }.AsQueryable());
            BidController controller = new BidController(mock.Object);
            controller.ModelState.Clear();

            //Act
            ActionResult result = controller.Add(new LotModel()
            {
                BidAmount = 10,
                Lot = mock.Object.Lots.First(),
            });

            // Assert 
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            

        }
        [TestMethod]
        public void Sell_Post_Validation_Erorr_Test()
        {
            //Arrange
            LotModel q = new LotModel {BidAmount = 10, Lot = new Lot
            {
                Name = "1",
                MinPrice = 100,
                CurrentPrice = 100,
                Bids = new List<Bid>(),
            }};
            //Act
            var context = new ValidationContext(q, null, null);
            var results = new List<ValidationResult>();
            //Assert
            var isModelStateValid = Validator.TryValidateObject(q, context, results, true);
            Assert.IsFalse(isModelStateValid);
        }
    }
}
