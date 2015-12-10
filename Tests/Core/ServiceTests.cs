using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using AlgoTrader.Core.AutoMapper;
using AlgoTrader.Core.Services;
using AlgoTrader.Core.Services.Interfaces;

namespace Test.Core
{
    [TestFixture]
    public class ServiceTests
    {
        private IYahooFinanceService _yahooFinanceService;
        private IFinancialCalculationService _financialCalculationService;

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
        [Ignore("YQL Options API no longer functional")]
        public void GetOptionData()
        {
            var optionData = _yahooFinanceService.GetOptionData("GOOG");
            Assert.IsNotEmpty(optionData);
        }

        /// <summary>
        /// The get option data with black scholes.
        /// </summary>
        [Test]
        [Ignore("YQL Options API no longer functional, no options data to work with")]
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