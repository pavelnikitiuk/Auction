using System.Collections.Generic;
using System.Linq;
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
    public class LotsControllerTest
    {
        [TestMethod]
        public void Paginate_Tes()
        {
            // Arrange
            Mock<ICategoriesRepository> category = new Mock<ICategoriesRepository>();
            category.Setup(m => m.Categories).Returns(new[]
            {
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Cat1",
                    Lots = new List<Lot>()
                },
                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Cat2",
                    Lots = new List<Lot>()
                },
                new Category
                {
                    CategoryId = 3,
                    CategoryName = "Cat3",
                    Lots = new List<Lot>()
                }
            }.AsQueryable());
            Mock<ILotsRepository> mock = new Mock<ILotsRepository>();
            mock.Setup(m => m.Lots).Returns(new[] {
            new Lot {LotID = 1, Name = "P1",IsCompleted = false},
            new Lot {LotID = 2, Name = "P2",IsCompleted = false},
            new Lot {LotID = 3, Name = "P3",IsCompleted = false},
            new Lot {LotID = 4, Name = "P4",IsCompleted = false},
          }.AsQueryable());
            LotsController controller = new LotsController(mock.Object, category.Object) { PageSize = 3 };

            // Act
            LotsListViewModel result = (LotsListViewModel)controller.List(null, 2).Model;

            //Assert
            Lot[] prodArray = result.Lots.ToArray();
            Assert.IsTrue(prodArray.Length == 1);
            Assert.AreEqual(prodArray[0].Name, "P4");
        }
        [TestMethod]
        public void Filter_Lots_Test()
        {
            // Arrange
            Mock<ICategoriesRepository> category = new Mock<ICategoriesRepository>();
            category.Setup(m => m.Categories).Returns(new[]
            {
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Cat1",
                    Lots = new List<Lot>()
                },
                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Cat2",
                    Lots = new List<Lot>()
                },
                new Category
                {
                    CategoryId = 3,
                    CategoryName = "Cat3",
                    Lots = new List<Lot>()
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

            category.Object.Categories.ToList()[1].Lots.Add(mock.Object.Lots.ToList()[1]);
            category.Object.Categories.ToList()[1].Lots.Add(mock.Object.Lots.ToList()[3]);

            LotsController controller = new LotsController(mock.Object, category.Object) { PageSize = 3 };

            // Action
            Lot[] result = ((LotsListViewModel)controller.List(2, 1).Model).Lots.ToArray();

            // Assert
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P2" && result[0].Category.CategoryName == "Cat2");
            Assert.IsTrue(result[1].Name == "P4" && result[1].Category.CategoryName == "Cat2");
        }
        [TestMethod]
        public void Lot_Count_Test()
        {
            // Arrange
            // - create the mock repository
            // Arrange
            Mock<ICategoriesRepository> category = new Mock<ICategoriesRepository>();
            category.Setup(m => m.Categories).Returns(new[]
            {
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Cat1",
                    Lots = new List<Lot>()
                },
                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Cat2",
                    Lots = new List<Lot>()
                },
                new Category
                {
                    CategoryId = 3,
                    CategoryName = "Cat3",
                    Lots = new List<Lot>()
                }
            }.AsQueryable());
            Mock<ILotsRepository> mock = new Mock<ILotsRepository>();
            mock.Setup(m => m.Lots).Returns(new[]
            {
                new Lot {LotID = 1, Name = "P1", Category = category.Object.Categories.ToList()[0],IsCompleted = false},
                new Lot {LotID = 2, Name = "P2", Category = category.Object.Categories.ToList()[1],IsCompleted = false},
                new Lot {LotID = 3, Name = "P3", Category = category.Object.Categories.ToList()[0],IsCompleted = false},
                new Lot {LotID = 4, Name = "P4", Category = category.Object.Categories.ToList()[1],IsCompleted = false},
                new Lot {LotID = 5, Name = "P5", Category = category.Object.Categories.ToList()[2],IsCompleted = false}
            }.AsQueryable());
            category.Object.Categories.ToList()[0].Lots.Add(mock.Object.Lots.ToList()[0]);
            category.Object.Categories.ToList()[1].Lots.Add(mock.Object.Lots.ToList()[1]);
            category.Object.Categories.ToList()[0].Lots.Add(mock.Object.Lots.ToList()[2]);
            category.Object.Categories.ToList()[1].Lots.Add(mock.Object.Lots.ToList()[3]);
            category.Object.Categories.ToList()[2].Lots.Add(mock.Object.Lots.ToList()[4]);
            // Arrange - create a controller and make the page size 3 items
            LotsController target = new LotsController(mock.Object, category.Object);
            target.PageSize = 3;

            // Action - test the Lot counts for different categories
            int res1 = ((LotsListViewModel)target
              .List(1).Model).PageModel.TotalItems;
            int res2 = ((LotsListViewModel)target
              .List(2).Model).PageModel.TotalItems;
            int res3 = ((LotsListViewModel)target
              .List(3).Model).PageModel.TotalItems;
            int resAll = ((LotsListViewModel)target
              .List(0).Model).PageModel.TotalItems;

            // Assert
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
        [TestMethod]
        public void Get_Image_Test()
        {
            // Arrange - create a Lot with image data
            Lot prod = new Lot
            {
                LotID = 2,
                Name = "Test",
                Images = new List<Image>
                {
                    new Image
                    {
                        ImageData = new byte[] {},
                        ImageMimeType = "image/png"
                    }
                }
            };
            // Arrange - create the mock repository
            Mock<ILotsRepository> mock = new Mock<ILotsRepository>();
            mock.Setup(m => m.Lots).Returns(new Lot[] {
          new Lot {LotID = 1, Name = "P1"},
          prod,
          new Lot {LotID = 3, Name = "P3"}
        }.AsQueryable());
            // Arrange - create the controller
            LotsController target = new LotsController(mock.Object, null);
            // Act - call the GetImage action method
            ActionResult result = target.GetImage(2, 0);
            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(prod.Images.First().ImageMimeType, ((FileResult)result).ContentType);
        }
        [TestMethod]
        public void Recive_Invalid_Id_Image_Test()
        {
            // Arrange 
            Mock<ILotsRepository> mock = new Mock<ILotsRepository>();
            mock.Setup(m => m.Lots).Returns(new Lot[]
            {
                new Lot {LotID = 1, Name = "P1"},
                new Lot {LotID = 2, Name = "P2"}
            }.AsQueryable());
            LotsController target = new LotsController(mock.Object, null);
            // Act 
            ActionResult result = target.GetImage(100, 0);
            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void SearchImage_Test()
        {
            // Arrange 
            Mock<ILotsRepository> mock = new Mock<ILotsRepository>();
            mock.Setup(m => m.Lots).Returns(new Lot[]
            {
                new Lot {LotID = 1, Name = "P1"},
                new Lot {LotID = 2, Name = "P2"},
                new Lot {LotID = 3, Name = "P2"},
                new Lot {LotID = 4, Name = "P2"},
                new Lot {LotID = 5, Name = "P2"},
                new Lot {LotID = 6, Name = "P2"},
                new Lot {LotID = 7, Name = "P2"}
            }.AsQueryable());
            LotsController target = new LotsController(mock.Object, null) { PageSize = 3 };

            //Act

            var vResult = target.SearchLot("P1") as ViewResult; ;
            var result = (LotsListViewModel)vResult.Model;
            //Assert

            Lot[] prodArray = result.Lots.ToArray();
            Assert.IsTrue(prodArray.Length == 1);
            Assert.AreEqual(prodArray[0].Name, "P1");
        }
        [TestMethod]
        public void Lot_Exist_Test()
        {
            // Arrange 
            Mock<ILotsRepository> mock = new Mock<ILotsRepository>();
            mock.Setup(m => m.Lots).Returns(new Lot[]
            {
                new Lot {LotID = 1, Name = "P1"},
                new Lot {LotID = 2, Name = "P2"},
                new Lot {LotID = 3, Name = "P2"},
                new Lot {LotID = 4, Name = "P2"},
                new Lot {LotID = 5, Name = "P2"},
                new Lot {LotID = 6, Name = "P2"},
                new Lot {LotID = 7, Name = "P2"}
            }.AsQueryable());
            LotsController target = new LotsController(mock.Object, null) { PageSize = 3 };

            //Act
            var result = target.Lot(1);
            var vResult = result as ViewResult;
            //Assert
            if (vResult == null)
                Assert.Fail();
            var model = (Lot)vResult.Model;
            Assert.AreEqual(model.Name, "P1");
        }
        [TestMethod]
        public void Lot_Unexist_Test()
        {
            // Arrange 
            Mock<ILotsRepository> mock = new Mock<ILotsRepository>();
            mock.Setup(m => m.Lots).Returns(new []
            {
                new Lot {LotID = 1, Name = "P1"},
            }.AsQueryable());
            LotsController target = new LotsController(mock.Object, null) { PageSize = 3 };

            //Act
            var result = target.Lot(null);
            var result1 = target.Lot(3);
            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            RedirectToRouteResult routeResult = result as RedirectToRouteResult;
            Assert.AreEqual(routeResult.RouteValues["action"], "List");

            Assert.IsInstanceOfType(result1, typeof(RedirectToRouteResult));
            RedirectToRouteResult routeResult1 = result1 as RedirectToRouteResult;
            Assert.AreEqual(routeResult1.RouteValues["action"], "List");
        }
        [TestMethod]
        public void Remove_Test()
        {
            // Arrange - create a Lot
            Lot prod = new Lot { LotID = 2, Name = "Test" };
            // Arrange - create the mock repository
            Mock<ILotsRepository> mock = new Mock<ILotsRepository>();
            mock.Setup(m => m.Lots).Returns(new Lot[]
            {
                new Lot {LotID = 1, Name = "P1"},
                prod,
                new Lot {LotID = 3, Name = "P3"},
            }.AsQueryable());
            // Arrange - create the controller
            LotsController target = new LotsController(mock.Object, null);
            // Act - delete the Lot
            target.Remove(prod.LotID, null);
            // Assert - ensure that the repository delete method was
            // called with the correct Lot
            mock.Verify(m => m.Remove(prod));
        }
    }
}
