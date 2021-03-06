﻿using System.Web.Mvc;
using System.Web.Routing;

namespace AlgoTrader.Portfolio
{
    /// <summary>
    ///     The route config.
    /// </summary>
    public static class RouteConfig
    {
        #region Public Methods and Operators

        /// <summary>
        /// The register routes.
        /// </summary>
        /// <param name="routes">
        /// The routes.
        /// </param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.LowercaseUrls = true;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", "{controller}/{action}", new { controller = "Main", action = "Index" });
        }

        #endregion
    }
}