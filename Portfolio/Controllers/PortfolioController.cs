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
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Core.DTOs;
    using Core.Services;
    using Core.Services.Interfaces;

    /// <summary>
    /// The portfolio controller.
    /// </summary>
    public class PortfolioController : Controller
    {
        #region Fields

        /// <summary>
        /// The _yahoo finance service.
        /// </summary>
        private readonly IYahooFinanceService _yahooFinanceService;

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
            this._yahooFinanceService = yahooFinanceService;
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
            YahooFinanceService.GoogleFinanceJSON[] results = this._yahooFinanceService.SymbolSearch(symbol).ToArray();
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
            IEnumerable<MarketDto> marketData = this._yahooFinanceService.GetData(symbol, null, null);
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
            IEnumerable<OptionDto> optionData = this._yahooFinanceService.GetData(symbol);
            return this.Json(optionData);
        }

        /// <summary>
        /// The options.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Options()
        {
            return this.View();
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