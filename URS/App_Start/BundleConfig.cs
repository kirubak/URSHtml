using System.Web;
using System.Web.Optimization;

namespace URS
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/lib/js/jquery.min.js",
                      "~/lib/js/bootstrap.min.js",
                      "~/js/main.js"
));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/lib/icomoon/style.css",
                      "~/css/reset.css",
                      "~/lib/css/bootstrap.min.css",
                      "~/lib/css/bootstrap-theme.min.css",
                      "~/css/mobileView.css",
                      "~/css/common.css",
                      "~/css/appStyle.css"
                      ));
        }
    }
}
