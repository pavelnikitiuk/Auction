using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Auction.Controllers;
using Auction.Domain.Abstract;
using Auction.Domain.Entities;
using Moq;

namespace Auction.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            Mock<ILotsRepository> mock = new Mock<ILotsRepository>();
            mock.Setup(m => m.Lots).Returns(new[] {
            new Lot {LotID = 1, Name = "P1",IsCompleted = false},
            new Lot {LotID = 2, Name = "P2",IsCompleted = false},
            new Lot {LotID = 3, Name = "P3",IsCompleted = false},
            new Lot {LotID = 4, Name = "P4",IsCompleted = false},
          }.AsQueryable());
            HomeController controller = new HomeController(mock.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController(null);

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController(null);

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
