namespace Test
{
    using Core;
    using Core.Services;
    using Core.Services.Interfaces;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Portfolio.Controllers;

    /// <summary>
    /// The controller tests.
    /// </summary>
    [TestClass]
    public class ControllerTests
    {
        #region Fields

        /// <summary>
        /// The _yahoo finance service.
        /// </summary>
        private readonly IYahooFinanceService yahooFinanceService = new YahooFinanceService();

        /// <summary>
        /// The financial calculation service.
        /// </summary>
        private readonly IFinancialCalculationService financialCalculationService = new FinancialCalculationService();

        /// <summary>
        /// The google finance service.
        /// </summary>
        private readonly IGoogleFinanceService googleFinanceService = new GoogleFinanceService(new WebRequestService());

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerTests"/> class.
        /// </summary>
        public ControllerTests()
        {
            AutoMapperConfig.Configure();
        }

        #endregion Constructor

        /// <summary>
        /// The option data test.
        /// </summary>
        [TestMethod]
        public void OptionDataTest()
        {
            var portfolioController = new PortfolioController(yahooFinanceService, googleFinanceService, financialCalculationService);
            var data = portfolioController.OptionData("MSFT", null, null);
            Assert.IsTrue(data.Data != null);
        }
    }
}