// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="">
//   
// </copyright>
// <summary>
//   The bundle config.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Portfolio.App_Start
{
    using System.Web.Optimization;

    /// <summary>
    /// The bundle config.
    /// </summary>
    public static class BundleConfig
    {
        #region Public Methods and Operators

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
                    "~/Content/Styles/angular-csp.css", 
                    "~/Content/Styles/bootstrap.css", 
                    "~/Content/Styles/toaster.css", 
                    "~/Content/Styles/spinner.css", 
                    "~/Content/Styles/circle.css", 
                    "~/Content/Styles/Gridmvc.css", 
                    "~/Content/Styles/animate.css"));

            // Custom CSS
            bundles.Add(new StyleBundle("~/Content/siteCss").Include("~/Content/Styles/Site.css"));

            // Required libraries
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/Modernizer/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/highStock").IncludeDirectory("~/Scripts/HighStock", "*.js", true));
            bundles.Add(new ScriptBundle("~/bundles/angular").Include("~/Scripts/Angular/angular.js"));
            bundles.Add(new ScriptBundle("~/bundles/angularRoute").Include("~/Scripts/Angular/angular-route.js"));
            bundles.Add(new ScriptBundle("~/bundles/angularAnimate").Include("~/Scripts/Angular/angular-animate.js"));
            bundles.Add(new ScriptBundle("~/bundles/angularResource").Include("~/Scripts/Angular/angular-resource.js"));
            bundles.Add(new ScriptBundle("~/bundles/angularScenario").Include("~/Scripts/Angular/angular-scenario.js"));
            bundles.Add(new ScriptBundle("~/bundles/angularLoader").Include("~/Scripts/Angular/angular-loader.js"));
            bundles.Add(new ScriptBundle("~/bundles/angularSanitize").Include("~/Scripts/Angular/angular-sanitize.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/Angular/ui-bootstrap-*"));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/Jquery/jquery-*"));
            bundles.Add(
                new ScriptBundle("~/bundles/jqueryVal").Include(
                    "~/Scripts/Jquery/Validate/jquery.unobtrusive*", 
                    "~/Scripts/Jquery/Validate/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/toast").Include("~/Scripts/Toast/toaster.js"));
            bundles.Add(new ScriptBundle("~/bundles/respond").Include("~/Scripts/Respond/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/underScore").Include("~/Scripts/UnderScore/underscore-min.js"));
            bundles.Add(new ScriptBundle("~/bundles/gridMvc").Include("~/Scripts/Grid/gridmvc.js"));

            // Created SPA JS Bundles
            bundles.Add(new ScriptBundle("~/bundles/angularApp").IncludeDirectory("~/Scripts/App", "AlgoTrader.js"));
            bundles.Add(new ScriptBundle("~/bundles/angularModules").IncludeDirectory("~/Scripts/App/Modules", "*.js"));
            bundles.Add(
                new ScriptBundle("~/bundles/angularDirectives").IncludeDirectory("~/Scripts/App/Directives", "*.js"));
            bundles.Add(
                new ScriptBundle("~/bundles/angularServices").IncludeDirectory("~/Scripts/App/Services", "*.js"));
            bundles.Add(
                new ScriptBundle("~/bundles/angularControllers").IncludeDirectory("~/Scripts/App/Controllers", "*.js"));
        }

        #endregion
    }
}