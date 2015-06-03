using System.Web.Mvc;
using System.Web.Routing;

namespace Auction
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null,
              "",
              new
              {
                  controller = "Home",
                  action = "Index",
                  category = (int?)null,
                  page = 1
              }
            );

            routes.MapRoute(null,
              "Page{page}",
              new { controller = "Lots", action = "List", categoryId = (int?)null },
              new { page = @"\d+" }
            );

            routes.MapRoute(null,
              "Category{categoryId}",
              new { controller = "Lots", action = "List", page = 1 }
            );

            routes.MapRoute(null,
              "Search_{search}",
              new { controller = "Lots", action = "SearchLot",page=1 }
            );

            routes.MapRoute(null,
              "Category{categoryId}/Page{page}",
              new { controller = "Lots", action = "List" },
              new { page = @"\d+" }
            );
            routes.MapRoute(null,
             "Search_{search}/Page{page}",
             new { controller = "Lots", action = "SearchLot" },
             new { page = @"\d+" }
           );

            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}
