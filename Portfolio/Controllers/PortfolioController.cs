using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AlgoTrader.Core.Services.Interfaces;

namespace AlgoTrader.Portfolio.Controllers
{
    /// <summary>
    /// The portfolio controller.
    /// </summary>
    public class PortfolioController : AsyncController
    {
        /// <summary>
        /// The google finance service.
        /// </summary>
        private readonly IGoogleFinanceService _googleFinanceService;

        /// <summary>
        /// The yahoo finance service.
        /// </summary>
        private readonly IYahooFinanceService _yahooFinanceService;

        /// <summary>
        /// The financial calculation service.
        /// </summary>
        private readonly IFinancialCalculationService _financialCalculationService;

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
            if (googleFinanceService == null)
                throw new ArgumentNullException(nameof(googleFinanceService));
            if (yahooFinanceService == null)
                throw new ArgumentNullException(nameof(yahooFinanceService));
            if (financialCalculationService == null)
                throw new ArgumentNullException(nameof(financialCalculationService));

            _yahooFinanceService = yahooFinanceService;
            _googleFinanceService = googleFinanceService;
            _financialCalculationService = financialCalculationService;
        }

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
            var results = _googleFinanceService.SymbolSearch(symbol).ToArray();
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
            var marketData = _yahooFinanceService.GetStockData(symbol);
            return Json(marketData);
        }

        /// <summary>
        /// The option data.
        /// </summary>
        /// <param name="symbol">
        /// The symbol.
        /// </param>
        /// <param name="from">
        /// </param>
        /// <param name="to">
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpPost]
        public JsonResult OptionData(string symbol, DateTime? from = null, DateTime? to = null)
        {
            var optionData = _yahooFinanceService.GetOptionData(symbol);

            Parallel.ForEach(optionData, dto =>
            {
                dto.Volatility = _financialCalculationService.Volatility(dto, from , to);
                dto.BlackScholes = _financialCalculationService.BlackScholes(dto);
            });

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
    }
}