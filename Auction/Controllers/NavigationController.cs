using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Auction.Domain.Abstract;

namespace Auction.Controllers
{
    public class NavigationController : Controller
    {
        private ILotsRepository lotsRepository;
        public NavigationController(ILotsRepository lotsRepository)
        {
            this.lotsRepository = lotsRepository;
        }

        public PartialViewResult Menu()
        {
            IEnumerable<string> categories = lotsRepository.Lots
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);

        }
    }
}