using Portfolio.App_Start;
using WebActivator;

[assembly: PreApplicationStartMethod(typeof(MunqConfig), "PreStart")]

namespace Portfolio.App_Start
{
    using Core.Services;
    using Core.Services.Interfaces;
    using Munq.MVC3;
    using System.Web.Mvc;

    /// <summary>
    /// The munq config.
    /// </summary>
    public static class MunqConfig
    {
        #region Public Methods and Operators

        /// <summary>
        /// The pre start.
        /// </summary>
        public static void PreStart()
        {
            DependencyResolver.SetResolver(new MunqDependencyResolver());
            var c = MunqDependencyResolver.Container;

            c.Register<IYahooFinanceService, YahooFinanceService>();
            c.Register<IGoogleFinanceService, GoogleFinanceService>();
            c.Register<IFinancialCalculationService, FinancialCalculationService>();
            c.Register<IWebRequestService, WebRequestService>();
        }

        #endregion
    }
}