using System;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Auction.Controllers;
using Auction.Infrastructure;
using Auction.ModelBinder;
using Auction.Models;
using Auction.Scheduler;

namespace Auction
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            NinjectControllerFactory factory = new NinjectControllerFactory();
            ControllerBuilder.Current.SetControllerFactory(factory);
            ModelBinders.Binders.DefaultBinder = new ModelBinder.ModelBinder();
            
           // JobScheduler.Start();

            
        }
    }
}
