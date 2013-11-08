﻿using System.Web.Optimization;

namespace Portfolio.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-buttons.css",
                "~/Content/Site.css",
                "~/Content/toaster.css",
                "~/Content/spinner.css"
                ));

            bundles.Add(new StyleBundle("~/Content/animationCss").Include(
                "~/Content/Animate/animate.css"));

            bundles.Add(new StyleBundle("~/Content/gridCss").Include(
                "~/Content/ng-grid.css"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/highChart").Include(
                "~/Scripts/HighCharts/highcharts.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/Angular/angular.js",
                "~/Scripts/Angular/angular-route.js",
                "~/Scripts/Angular/angular-animate.js",
                "~/Scripts/Angular/angular-resource.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/Angular/ui-bootstrap-*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/Jquery/jquery-*"));

            bundles.Add(new ScriptBundle("~/bundles/toast").Include(
                "~/Scripts/Toast/toaster.js"));

            bundles.Add(new ScriptBundle("~/bundles/respond").Include(
                "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/underScore").Include(
                "~/Scripts/UnderScore/underscore-min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularApp")
                .IncludeDirectory("~/Scripts/App", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularModules")
                .IncludeDirectory("~/Scripts/App/Modules", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularDirectives")
                .IncludeDirectory("~/Scripts/App/Directives", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularServices")
                .IncludeDirectory("~/Scripts/App/Services", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularControllers")
                .IncludeDirectory("~/Scripts/App/Controllers", "*.js"));
        }
    }
}