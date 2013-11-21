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
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Grid(IEnumerable<OptionDto> data) 
        {
            return this.PartialView("_OptionTable", data);
        }

        #endregion
    }
}