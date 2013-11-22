// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RouteConfig.cs" company="">
//   
// </copyright>
// <summary>
//   The route config.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Portfolio.App_Start
{
    using System.Web.Mvc;
    using System.Web.Routing;

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

            routes.MapRoute("MainRoute", "Main/{*all}", new { controller = "Home", all = UrlParameter.Optional });
        }

        #endregion
    }
}