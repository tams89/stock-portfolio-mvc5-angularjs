// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainController.cs" company="">
//   
// </copyright>
// <summary>
//   The main controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Portfolio.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// The main controller.
    /// </summary>
    public class MainController : Controller
    {
        #region Public Methods and Operators

        /// <summary>
        /// The about.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult About()
        {
            return this.View();
        }

        /// <summary>
        /// The home.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Home()
        {
            return this.View("Home");
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            return this.View("Index");
        }

        /// <summary>
        /// The login.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Login()
        {
            return this.View("Login");
        }

        /// <summary>
        /// The register.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Register()
        {
            return this.View("Register");
        }

        #endregion
    }
}