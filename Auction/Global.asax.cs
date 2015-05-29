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
           //Database.SetInitializer<ApplicationDbContext>(null);
           //Database.SetInitializer<ApplicationDbContext>(new AppDbInitializer());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            NinjectControllerFactory factory = new NinjectControllerFactory();
            ControllerBuilder.Current.SetControllerFactory(factory);
            Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());

            ModelBinders.Binders.DefaultBinder = new XSSModelBinder();
            
           // JobScheduler.Start();

            //var timer = new System.Timers.Timer() { Interval = 60000 };
            //timer.Elapsed += (sender, args) => Workflow.LotEndChecker.SearchEndedLots();
            //timer.Start();
        }
    }
}
