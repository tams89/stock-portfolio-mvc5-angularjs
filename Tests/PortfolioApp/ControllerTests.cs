using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AlgoTrader.Core.AutoMapper;
using AlgoTrader.Core.DTO;
using AlgoTrader.Core.Services;
using AlgoTrader.Core.Services.Interfaces;
using AlgoTrader.Portfolio.Controllers;

namespace Test.PortfolioApp
{
    /// <summary>
    /// Controller integration tests.
    /// </summary>
    [TestFixture]
    public class ControllerTests
    {
        #region Fields

        /// <summary>
        /// The _yahoo finance service.
        /// </summary>
        private IYahooFinanceService _yahooFinanceService;

        /// <summary>
        /// The financial calculation service.
        /// </summary>
        private IFinancialCalculationService _financialCalculationService;

        /// <summary>
        /// The google finance service.
        /// </summary>
        private IGoogleFinanceService _googleFinanceService;

        /// <summary>
        /// The Portfolio Controller.
        /// </summary>
        private PortfolioController _portfolioController;

        #endregion Fields

        [SetUp]
        public void Init()
        {
            AutoMapperConfig.Configure();

            _yahooFinanceService = new YahooFinanceService();
            _financialCalculationService = new FinancialCalculationService();
            _googleFinanceService = new GoogleFinanceService(new WebRequestService());
            _portfolioController = new PortfolioController(_yahooFinanceService, _googleFinanceService, _financialCalculationService);
        }

        /// <summary>
        /// The option data test.
        /// Note datetime format YYYY/MM/DD
        /// </summary>
        [TestCase("MSFT", null, null)]
        [TestCase("MSFT", "1900/01/01", "2000/01/01")]
        [TestCase("MSFT", "2000/01/01", "1990/01/01")]
        public void PortfolioController_DoesGetOptionData_FromService(string symbol, string from, string to)
        {
            JsonResult data;

            if (from != null && to != null)
            {
                var fromDate = DateTime.Parse(from);
                var toDate = DateTime.Parse(to);
                data = _portfolioController.OptionData(symbol, fromDate, toDate);
            }
            else
            {
                data = _portfolioController.OptionData(symbol);
            }

            Assert.IsTrue(data.Data != null);
        }

        /// <summary>
        /// The option data null test.
        /// </summary>
        [TestCase("", "2000/01/01", "1990/01/01")]
        public void PortfolioController_DoesntGetOptionData_FromService(string symbol, string from, string to)
        {
            var fromDate = DateTime.Parse(from);
            var toDate = DateTime.Parse(to);
            var data = _portfolioController.OptionData(symbol, fromDate, toDate);
            Assert.IsTrue(!((IEnumerable<OptionDto>)data.Data).Any());
        }


        /// <summary>
        /// Controller Autocomplete test.
        /// </summary>
        [TestCase("APPL")]
        [TestCase("GOOG")]
        public void PortfolioController_DoesGetAutocompleteData_FromService(string symbol)
        {
            var data = _portfolioController.AutoComplete(symbol);
            Assert.IsTrue(data.Data != null);
        }

        /// <summary>
        /// Controller Autocomplete test.
        /// </summary>
        [TestCase("")]
        public void PortfolioController_DoesntGetAutocompleteData_FromService(string symbol)
        {
            var data = _portfolioController.AutoComplete(symbol);
            Assert.IsTrue(data == null);
        }

        /// <summary>
        /// Controller MarketData test.
        /// </summary>
        [TestCase("APPL")]
        [TestCase("GOOG")]
        public void PortfolioController_DoesGetMarketData_FromService(string symbol)
        {
            var data = _portfolioController.MarketData(symbol);
            Assert.IsTrue(data.Data != null);
        }

        /// <summary>
        /// Controller MarketData test.
        /// </summary>
        [TestCase("")]
        public void PortfolioController_DoesntGetMarketData_FromService(string symbol)
        {
            var data = _portfolioController.MarketData(symbol);
            Assert.IsTrue(!((IEnumerable<MarketDto>)data.Data).Any());
        }
    }
}