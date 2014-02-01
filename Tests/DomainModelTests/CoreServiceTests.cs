using Core;

namespace Test
{
    using Core.Services;
    using Core.Services.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    /// <summary>
    /// The yahoo finance service tests.
    /// </summary>
    [TestClass]
    public class CoreServiceTests
    {
        #region Fields

        /// <summary>
        /// The _yahoo finance service.
        /// </summary>
        private IYahooFinanceService yahooFinanceService;

        /// <summary>
        /// The financial calculation service.
        /// </summary>
        private IFinancialCalculationService financialCalculationService;

        #endregion Fields

        [TestInitialize]
        public void Init()
        {
            AutoMapperConfig.Configure();
            yahooFinanceService = new YahooFinanceService();
            financialCalculationService = new FinancialCalculationService();
        }

        /// <summary>
        /// The get stock data.
        /// </summary>
        [TestMethod]
        public void GetStockData()
        {
            var marketData = yahooFinanceService.GetStockData("MSFT", null, null);
            Assert.IsNotNull(marketData);
        }

        /// <summary>
        /// The get option data.
        /// </summary>
        [TestMethod]
        public void GetOptionData()
        {
            var optionData = yahooFinanceService.GetOptionData("GOOG");
            Assert.IsNotNull(optionData);
        }

        /// <summary>
        /// The get option data with black scholes.
        /// </summary>
        [TestMethod]
        public void GetOptionDataWithBlackScholes()
        {
            var optionData = yahooFinanceService.GetOptionData("MSFT").ToList();

            foreach (var optionDto in optionData)
            {
                optionDto.Volatility = financialCalculationService.Volatility(optionDto, null, null);
                optionDto.BlackScholes = financialCalculationService.BlackScholes(optionDto);
            }

            Assert.IsTrue(optionData.Any(x => x.BlackScholes > 1 && x.Volatility > 0.001));
        }
    }
}