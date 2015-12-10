using System.Globalization;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AlgoTrader.Portfolio.Controllers;

namespace AlgoTrader.Portfolio
{
    /// <summary>
    /// The error config.
    /// </summary>
    public class ErrorConfig
    {
        /// <summary>
        /// The handler.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public static void Handle(HttpContext context)
        {
            switch (context.Response.StatusCode)
            {
                // Not authorized.
                case 401:
                    Show(context, 401);
                    break;

                // Not found.
                case 404:
                    Show(context, 404);
                    break;
            }
        }

        /// <summary>
        /// The show.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="code">
        /// The code.
        /// </param>
        private static void Show(HttpContext context, int code)
        {
            context.Response.Clear();

            var w = new HttpContextWrapper(context);
            var c = new ErrorController() as IController;
            var rd = new RouteData();

            rd.Values["controller"] = "Error";
            rd.Values["action"] = "Index";
            rd.Values["id"] = code.ToString(CultureInfo.InvariantCulture);

            c.Execute(new RequestContext(w, rd));
        }
    }
}