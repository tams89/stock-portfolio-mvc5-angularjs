using System.Web.Mvc;
using System.Web.Routing;

namespace Portfolio.App_Start
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", "{controller}/{action}",
                new { controller = "Main", action = "Index" }
                );

            routes.MapRoute("AlgoTrader", "AlgoTrader/{controller}/{action}",
                new { controller = "Main", action = "Index" }
                );
        }
    }
}