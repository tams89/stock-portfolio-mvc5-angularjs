using System.Web.Optimization;

namespace Portfolio.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/angularbootstrap").Include(
                      "~/Scripts/angular.js",
                      "~/Scripts/angular-*",
                      "~/Scripts/ui-bootstrap-*",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-buttons.css",
                      "~/Content/Site.css"));
        }
    }
}
