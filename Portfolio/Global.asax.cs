using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AlgoTrader.Core.AutoMapper;
using AlgoTrader.Portfolio.DependencyResolution;

namespace AlgoTrader.Portfolio
{
    /// <summary>
    /// The mvc application initialising members.
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        #region Methods

        /// <summary>
        /// The application_ start.
        /// </summary>
        protected void Application_Start()
        {
            // This prevents mvc from searching for .aspx pages in a razor based project by removing the engine. Performance?
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            MvcHandler.DisableMvcResponseHeader = true; // Security?

            StructuremapMvc.Start(); // IoC Setup.
            AutoMapperConfig.Configure();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// The end of a request.
        /// </summary>
        /// <param name="sender">
        /// </param>
        /// <param name="e">
        /// </param>
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            ErrorConfig.Handle(Context);
        }

        #endregion
    }
}