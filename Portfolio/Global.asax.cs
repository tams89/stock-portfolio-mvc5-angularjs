// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="">
//   
// </copyright>
// <summary>
//   The mvc application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Portfolio
{
    using System;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using App_Start;

    /// <summary>
    /// The mvc application.
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        #region Methods

        /// <summary>
        /// The application_ start.
        /// </summary>
        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            
            MvcHandler.DisableMvcResponseHeader = true;

            MunqConfig.PreStart(); // IoC
            
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
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