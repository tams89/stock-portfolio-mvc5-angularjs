using System.Web.Mvc;
using Core.Services;
using Core.Services.Interfaces;
using Munq.MVC3;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Portfolio.App_Start.MunqConfig), "PreStart")]
namespace Portfolio.App_Start
{
    public static class MunqConfig
    {
        public static void PreStart()
        {
            DependencyResolver.SetResolver(new MunqDependencyResolver());
            var c = MunqDependencyResolver.Container;
            
            c.Register<IYahooFinanceService, YahooFinanceService>();
        }
    }
}
