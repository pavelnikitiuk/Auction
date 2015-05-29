//using System;
//using System.Linq;
//using Auction.Controllers;
//using Auction.Domain.Abstract;
//using Auction.Domain.Entities;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;

//namespace Auction.Tests.Controllers
//{
//    [TestClass]
//    public class NavigationControllerTest
//    {
//        [TestMethod]
//        public void Selected_Category()
//        {
//            // Arrange
//            Mock<ICategoriesRepository> category = new Mock<ICategoriesRepository>();
//            category.Setup(m => m.Categories).Returns(new[]
//            {
//                new Category
//                {
//                    CategoryId = 1,
//                    CategoryName = "Cat1",
//                },
//                new Category
//                {
//                    CategoryId = 1,
//                    CategoryName = "Cat2",
//                },
//                new Category
//                {
//                    CategoryId = 1,
//                    CategoryName = "Cat3",
//                }
//            }.AsQueryable());
//            Mock<ILotsRepository> mock = new Mock<ILotsRepository>();
//            mock.Setup(m => m.Lots).Returns(new Lot[]
//            {
//                new Lot {LotID = 1, Name = "P1", Category = category.Object.Categories.ToList()[0]},
//                new Lot {LotID = 4, Name = "P2", Category = category.Object.Categories.ToList()[1]},
//            }.AsQueryable());
//            category.Object.Categories.ToList()[0].Lots.Add(mock.Object.Lots.ToList()[0]);
//            category.Object.Categories.ToList()[1].Lots.Add(mock.Object.Lots.ToList()[1]);
//            // Arrange - create the controller
//            NavigationController target = new NavigationController(category.Object);

//            // Arrange - define the category to selected
//            string categoryToSelect = "Cat1";

//            // Action
////            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

//            // Assert
//  //          Assert.AreEqual(categoryToSelect, result);
//        }
//    }
//}
