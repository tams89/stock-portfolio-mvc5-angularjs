using Core;
using Core.Services;
using Core.Services.Interfaces;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Test.DomainModelTests
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
        private IYahooFinanceService yahooFinanceService;

        /// <summary>
        /// The financial calculation service.
        /// </summary>
        private IFinancialCalculationService financialCalculationService;

        #endregion Fields

        [SetUp]
        public void Init()
        {
            AutoMapperConfig.Configure();
            yahooFinanceService = new YahooFinanceService();
            financialCalculationService = new FinancialCalculationService();
        }

        /// <summary>
        /// The get stock data.
        /// </summary>
        [Test]
        public void GetStockData()
        {
            var marketData = yahooFinanceService.GetStockData("MSFT", null, null);
            Assert.IsNotNull(marketData);
        }

        /// <summary>
        /// The get option data.
        /// </summary>
        [Test]
        public void GetOptionData()
        {
            var optionData = yahooFinanceService.GetOptionData("GOOG");
            Assert.IsNotNull(optionData);
        }

        /// <summary>
        /// The get option data with black scholes.
        /// </summary>
        [Test]
        public void Does_BlackScholes_GetCalculated()
        {
            var optionData = yahooFinanceService.GetOptionData("MSFT").ToList();

            Parallel.ForEach(optionData, dto =>
            {
                dto.Volatility = financialCalculationService.Volatility(dto, null, null);
                dto.BlackScholes = financialCalculationService.BlackScholes(dto);
            });

            Assert.IsTrue(optionData.Any(x => x.BlackScholes > 1 && x.Volatility > 0.001));
        }
    }
}