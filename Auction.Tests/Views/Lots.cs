﻿using System;
using System.Collections.Generic;
using System.Linq;
using Auction.Controllers;
using Auction.Domain.Abstract;
using Auction.Domain.Entities;
using Auction.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Auction.Tests.Views
{
    [TestClass]
    public class Lots
    {
        [TestMethod]
        public void ListView_Model()
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
            mock.Setup(m => m.Lots).Returns(new Lot[]
            {
                new Lot {LotID = 1, Name = "P1"},
                new Lot {LotID = 2, Name = "P2"},
                new Lot {LotID = 3, Name = "P3"},
                new Lot {LotID = 4, Name = "P4"},
                new Lot {LotID = 5, Name = "P5"}
            }.AsQueryable());


            LotsController controller = new LotsController(mock.Object, category.Object);
            controller.PageSize = 3;

            // Act
            LotsListViewModel result = (LotsListViewModel)controller.List(null, 2).Model;

            // Assert
            PageModel pageInfo = result.PageModel;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }
    }
}
