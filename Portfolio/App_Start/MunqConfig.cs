using System.Web.Mvc;
using Core.Services;
using Core.Services.Interfaces;
using Munq.MVC3;
using Portfolio.App_Start;
using WebActivator;

[assembly: PreApplicationStartMethod(typeof (MunqConfig), "PreStart")]

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