// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GridController.cs" company="">
//   
// </copyright>
// <summary>
//   The grid controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Portfolio.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Core.DTO;

    /// <summary>
    /// The grid controller.
    /// </summary>
    public class GridController : Controller
    {
        #region Public Methods and Operators

        /// <summary>
        /// The grid.
        /// </summary>
        /// <param name="data">
        /// Collection containing data.
        /// </param>
        /// <typeparam name="T">
        /// The DTO.
        /// </typeparam>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Grid<T>(IEnumerable<T> data) where T : DtoBase
        {
            return this.PartialView("_OptionTable", data);
        }

        #endregion
    }
}