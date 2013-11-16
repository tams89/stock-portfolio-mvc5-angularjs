using System.Web.Optimization;

namespace Portfolio.App_Start
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/Styles/angular-csp.css",
                "~/Content/Styles/bootstrap.css",
                "~/Content/Styles/bootstrap-buttons.css",
                "~/Content/Styles/toaster.css",
                "~/Content/Styles/spinner.css",
                "~/Content/Styles/circle.css",
                "~/Content/Styles/ng-grid.css",
                "~/Content/Styles/animate.css"
                ));

            bundles.Add(new StyleBundle("~/Content/siteCss").Include(
                "~/Content/Styles/Site.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/Modernizer/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/ajaxLogin").Include(
                "~/Scripts/App/ajaxLogin.js"));

            bundles.Add(new ScriptBundle("~/bundles/highStock")
                .IncludeDirectory("~/Scripts/HighStock", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/Angular/angular.js",
                "~/Scripts/Angular/angular-route.js",
                "~/Scripts/Angular/angular-animate.js",
                "~/Scripts/Angular/angular-resource.js",
                "~/Scripts/Angular/angular-scenario.js",
                "~/Scripts/Angular/angular-loader.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/Angular/ui-bootstrap-*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/Jquery/jquery-*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryVal").Include(
                "~/Scripts/Jquery/Validate/jquery.unobtrusive*",
                "~/Scripts/Jquery/Validate/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/toast").Include(
                "~/Scripts/Toast/toaster.js"));

            bundles.Add(new ScriptBundle("~/bundles/respond").Include(
                "~/Scripts/Respond/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/underScore").Include(
                "~/Scripts/UnderScore/underscore-min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularApp")
                .IncludeDirectory("~/Scripts/App", "AlgoTrader.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularModules")
                .IncludeDirectory("~/Scripts/App/Modules", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularDirectives")
                .IncludeDirectory("~/Scripts/App/Directives", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularServices")
                .IncludeDirectory("~/Scripts/App/Services", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularControllers")
                .IncludeDirectory("~/Scripts/App/Controllers", "*.js"));

            BundleTable.EnableOptimizations = false;
        }
    }
}