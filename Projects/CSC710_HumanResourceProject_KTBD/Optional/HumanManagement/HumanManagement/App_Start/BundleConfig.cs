using System.Web;
using System.Web.Optimization;

namespace HumanManagement
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/js/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/fa/css/fontawesome.css"));

            bundles.Add(new StyleBundle("~/Content/fa").Include(
                      "~/Content/fa/css/fontawesome.css"));

            bundles.Add(new StyleBundle("~/Content/Treant").Include(
                      "~/Content/Treant.css"));

            bundles.Add(new ScriptBundle("~/bundles/treant").Include(
                        "~/Scripts/Treant.js",
                        "~/Scripts/vendor/raphael.js"));
        }
    }
}
