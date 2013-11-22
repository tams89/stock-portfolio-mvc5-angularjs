// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorController.cs" company="">
//   
// </copyright>
// <summary>
//   The error controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Portfolio.Controllers
{
    using System;
    using System.Web.Mvc;

    /// <summary>
    /// The error controller.
    /// </summary>
    public class ErrorController : Controller
    {
        // GET: /Error/
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
        [HttpGet]
        public ActionResult Index(int? id)
        {
            var statusCode = id.HasValue ? id.Value : 500;
            var error =
                new HandleErrorInfo(
                    new Exception(string.Format("Wooo, this is embarrassing, a http {0} error occured.", statusCode)), 
                    "Error", 
                    "Index");
            return View("Error", error);
        }

        #endregion
    }
}