using System.Web.Optimization;

namespace AlgoTrader.Portfolio
{
    /// <summary>
    /// The bundle config.
    /// </summary>
    public static class BundleConfig
    {
        /// <summary>
        /// The register bundles.
        /// </summary>
        /// <param name="bundles">
        /// The bundles.
        /// </param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Library CSS
            bundles.Add(
                new StyleBundle("~/Content/css").Include(
                    "~/Content/Styles/angular-csp.min.css",
                    "~/Content/Styles/bootstrap.min.css",
                    "~/Content/Styles/toaster.min.css",
                    "~/Content/Styles/spinner.min.css",
                    "~/Content/Styles/circle.min.css",
                    "~/Content/Styles/ng-grid.min.css",
                    "~/Content/Styles/animate.min.css"));

            // Custom CSS
            bundles.Add(new StyleBundle("~/Content/siteCss").Include("~/Content/Styles/Site.css"));

            // Required libraries
            bundles.Add(new ScriptBundle("~/bundles/highStock")
                .Include("~/Scripts/HighStock/highstock.min.js")
                .Include("~/Scripts/Highcharts-4.0.1/highcharts.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/Angular/angular.js",
                "~/Scripts/Angular/angular-route.js",
                "~/Scripts/Angular/angular-animate.js",
                "~/Scripts/Angular/angular-resource.js",
                "~/Scripts/Angular/angular-scenario.js",
                "~/Scripts/Angular/angular-loader.js",
                "~/Scripts/Angular/angular-sanitize.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/Angular/ui-bootstrap-*"));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-*"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryVal").Include("~/Scripts/jquery.unobtrusive*", "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/toast").Include("~/Scripts/Toast/toaster.js"));
            bundles.Add(new ScriptBundle("~/bundles/underScore").Include("~/Scripts/UnderScore/underscore-min.js"));

            // Created SPA JS Bundles
            bundles.Add(new ScriptBundle("~/bundles/angularApp")
                .IncludeDirectory("~/Scripts/_App", "AlgoTrader.js")
                .IncludeDirectory("~/Scripts/_App/Modules", "*.js")
                .IncludeDirectory("~/Scripts/_App/Directives", "*.js")
                .IncludeDirectory("~/Scripts/_App/Services", "*.js")
                .IncludeDirectory("~/Scripts/_App/Controllers", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/NoAuth")
                .Include("~/Scripts/_App/NoAuth.min.js")
                .Include("~/Scripts/_App/Services/authenticationService.min.js")
                .Include("~/Scripts/_App/Controllers/NavigationController.min.js")
                .Include("~/Scripts/_App/Controllers/LoginController.min.js")
                .Include("~/Scripts/_App/Controllers/RegistrationController.min.js"));
        }
    }
}