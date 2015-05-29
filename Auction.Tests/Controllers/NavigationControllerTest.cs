using System;
using System.Linq;
using Auction.Controllers;
using Auction.Domain.Abstract;
using Auction.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Auction.Tests.Controllers
{
    [TestClass]
    public class NavigationControllerTest
    {
        [TestMethod]
        public void Selected_Category()
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
            

            // Act
            var target = new NavigationController(category.Object);
            target.Menu();

            // Assert
        }
    }
}
