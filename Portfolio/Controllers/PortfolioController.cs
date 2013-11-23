// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PortfolioController.cs" company="">
//   
// </copyright>
// <summary>
//   The portfolio controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Portfolio.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Core.DTO;
    using Core.Services.Interfaces;

    /// <summary>
    /// The portfolio controller.
    /// </summary>
    public class PortfolioController : Controller
    {
        #region Fields

        /// <summary>
        /// The google finance service.
        /// </summary>
        private readonly IGoogleFinanceService googleFinanceService;

        /// <summary>
        /// The yahoo finance service.
        /// </summary>
        private readonly IYahooFinanceService yahooFinanceService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PortfolioController"/> class.
        /// </summary>
        /// <param name="yahooFinanceService">
        /// The yahoo finance service.
        /// </param>
        /// <param name="googleFinanceService">
        /// </param>
        public PortfolioController(IYahooFinanceService yahooFinanceService, IGoogleFinanceService googleFinanceService)
        {
            this.yahooFinanceService = yahooFinanceService;
            this.googleFinanceService = googleFinanceService;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The auto complete.
        /// </summary>
        /// <param name="symbol">
        /// The symbol.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpPost]
        public JsonResult AutoComplete(string symbol)
        {
            var results = googleFinanceService.SymbolSearch(symbol).ToArray();
            return results.Any() ? Json(results.Select(x => new { x.Name, x.Symbol })) : null;
        }

        /// <summary>
        /// The market data.
        /// </summary>
        /// <param name="symbol">
        /// The symbol.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpPost]
        public JsonResult MarketData(string symbol)
        {
            var marketData = yahooFinanceService.GetData(symbol, null, null);
            return Json(marketData);
        }

        /// <summary>
        /// The option data.
        /// </summary>
        /// <param name="symbol">
        /// The symbol.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpPost]
        public JsonResult OptionData(string symbol)
        {
            var optionData = yahooFinanceService.GetData(symbol);
            return Json(optionData);
        }

        /// <summary>
        /// The options.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Options()
        {
            return View();
        }

        /// <summary>
        /// The option table update.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public ActionResult OptionTableUpdate(string symbol)
        {
            var optionTable = yahooFinanceService.GetData(symbol);
            return PartialView("_OptionTable", optionTable);
        }

        /// <summary>
        ///     The stocks.
        /// </summary>
        /// <returns>
        ///     The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Stocks()
        {
            return View();
        }

        #endregion
    }
}