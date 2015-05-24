using System.Web;
using System.Web.Optimization;

namespace Auction
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js", "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            //bundles.Add(new ScriptBundle("~/bundles/clock").Include(
            //          "~/Scripts/custom.js",
            //          "~/Scripts/plugins.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/bootsrapdate").Include(
                        "~/Scripts/jquery-ui-{version}.js").Include("~/Scripts/moment.js").Include(
                        "~/Scripts/bootstrap-datetimepicker.js"));
            
            


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/bdate").Include(
                     "~/Content/bootstrap-datetimepicker.css"));

            bundles.Add(new StyleBundle("~/Content/soon").Include(
                     "~/Content/soon.css"));
            bundles.Add(new StyleBundle("~/Content/shop").Include(
                     "~/Content/shop-homepage.css"));

            bundles.Add(new StyleBundle("~/Content/bootsrap-social").Include(
                      "~/Content/bootstrap-social.css", "~/Content/font-awesome.css"));
            
            

        }
    }
}
