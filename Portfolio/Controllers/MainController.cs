using System.Web.Mvc;

namespace AlgoTrader.Portfolio.Controllers
{
    /// <summary>
    /// The main controller.
    /// </summary>
    public class MainController : Controller
    {
        /// <summary>
        /// The about.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// The home.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Home()
        {
            return View("Home");
        }

        /// Tesdt

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}