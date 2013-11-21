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

    using Core.Services.Interfaces;

    using Portfolio.Attributes;

    /// <summary>
    /// The portfolio controller.
    /// </summary>
    public class PortfolioController : Controller
    {
        #region Fields

        /// <summary>
        /// The _yahoo finance service.
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
        public PortfolioController(IYahooFinanceService yahooFinanceService)
        {
            this.yahooFinanceService = yahooFinanceService;
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
            var results = this.yahooFinanceService.SymbolSearch(symbol).ToArray();
            if (!results.Any())
            {
                return null;
            }

            var json = results.Select(x => new { x.Name, x.Symbol });
            return this.Json(json);
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
            var marketData = this.yahooFinanceService.GetData(symbol, null, null);
            return this.Json(marketData);
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
            var optionData = this.yahooFinanceService.GetData(symbol);
            return this.Json(optionData);
        }

        /// <summary>
        /// The options.
        /// </summary>
        /// <param name="symbol">
        /// The symbol.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Options(string symbol)
        {
            if (string.IsNullOrEmpty(symbol)) return this.View();
            var optionData = this.yahooFinanceService.GetData(symbol);
            return this.View(optionData);
        }

        /// <summary>
        /// The stocks.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Stocks()
        {
            return this.View();
        }

        #endregion
    }
}