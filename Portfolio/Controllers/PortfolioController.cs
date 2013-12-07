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
    using Core.DTO;
    using Core.Services.Interfaces;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;


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

        /// <summary>
        /// The financial calculation service.
        /// </summary>
        private readonly IFinancialCalculationService financialCalculationService;

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
        /// <param name="financialCalculationService">
        /// The financial Calculation Service.
        /// </param>
        public PortfolioController(IYahooFinanceService yahooFinanceService, IGoogleFinanceService googleFinanceService, IFinancialCalculationService financialCalculationService)
        {
            this.yahooFinanceService = yahooFinanceService;
            this.googleFinanceService = googleFinanceService;
            this.financialCalculationService = financialCalculationService;
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
            var optionData = (List<OptionDto>)yahooFinanceService.GetData(symbol);
            foreach (var optionDto in optionData) optionDto.BlackScholes = financialCalculationService.BlackScholes(optionDto, null, null);
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