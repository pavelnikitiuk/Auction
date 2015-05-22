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
        private ICategoriesRepository categoriesRepository;
        public NavigationController(ICategoriesRepository categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public PartialViewResult Menu()
        {
            IEnumerable<string> categories =
                categoriesRepository.Categories.Select(x => x.CategoryName).OrderBy(x => x);
            return PartialView(categories);

        }
    }
}