using System;
using System.Web.Mvc;

namespace AlgoTrader.Portfolio.Controllers
{
    /// <summary>
    /// The error controller.
    /// </summary>
    public class ErrorController : Controller
    {
        #region Public Methods and Operators

        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index(int? id)
        {
            var statusCode = id ?? 500;
            var error =
                new HandleErrorInfo(
                    new Exception($"Wooo, this is embarrassing, a http {statusCode} error occured."),
                    "Error",
                    "Index");
            return View("Error", error);
        }

        #endregion
    }
}