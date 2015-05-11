using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Auction.Domain.Abstract;
using Auction.Domain.Entities;

namespace Auction.Controllers
{
    public class LotsController : Controller
    {
        private ILotsRepository repository;
        public LotsController(ILotsRepository productRepository)
        {
            repository = productRepository;
        }

        public ViewResult List()
        {
            return View(repository.Lots);
        }
    }
}