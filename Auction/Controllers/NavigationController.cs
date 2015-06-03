using System.Linq;
using System.Web.Mvc;
using Auction.Domain.Abstract;
using Auction.Models;

namespace Auction.Controllers
{
    public class NavigationController : Controller
    {
        private ICategoriesRepository categoriesRepository;
        public NavigationController(ICategoriesRepository categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }
        /// <summary>
        /// Naviagation menu
        /// </summary>
        /// <returns>Partial view to navigation</returns>
        public PartialViewResult Menu()
        {
            var categories =
                categoriesRepository.Categories.Select(x =>new MenuModel{CategoryId = x.CategoryId, CategoryName = x.CategoryName}).OrderBy(x => x);

            return PartialView(categories);

        }
    }
}