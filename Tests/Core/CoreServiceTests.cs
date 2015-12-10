using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using AlgoTrader.Core.AutoMapper;
using AlgoTrader.Core.Services;
using AlgoTrader.Core.Services.Interfaces;

namespace Test.Core
{
    /// <summary>
    /// The yahoo finance service tests.
    /// </summary>
    [TestFixture]
    public class CoreServiceTests
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

        #endregion Fields

        [SetUp]
        public void Init()
        {
            AutoMapperConfig.Configure();
            _yahooFinanceService = new YahooFinanceService();
            _financialCalculationService = new FinancialCalculationService();
        }

        /// <summary>
        /// The get stock data.
        /// </summary>
        [Test]
        public void GetStockData()
        {
            var marketData = _yahooFinanceService.GetStockData("MSFT");
            Assert.IsNotNull(marketData);
        }

        /// <summary>
        /// The get option data.
        /// </summary>
        [Test]
        public void GetOptionData()
        {
            var optionData = _yahooFinanceService.GetOptionData("GOOG");
            Assert.IsNotNull(optionData);
        }

        /// <summary>
        /// The get option data with black scholes.
        /// </summary>
        [Test]
        public void Does_BlackScholes_GetCalculated()
        {
            var optionData = _yahooFinanceService.GetOptionData("MSFT").ToList();

            Parallel.ForEach(optionData, dto =>
            {
                dto.Volatility = _financialCalculationService.Volatility(dto);
                dto.BlackScholes = _financialCalculationService.BlackScholes(dto);
            });

            Assert.IsTrue(optionData.Any(x => x.BlackScholes > 1 && x.Volatility > 0.001));
        }
    }
}