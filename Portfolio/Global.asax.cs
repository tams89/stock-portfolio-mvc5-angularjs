using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Portfolio.App_Start;

namespace Portfolio
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            MunqConfig.PreStart(); // IoC
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}