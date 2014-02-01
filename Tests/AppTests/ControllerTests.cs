using Core;
using Core.Services;
using Core.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portfolio.Controllers;

namespace Test.AppTests
{
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
        private IYahooFinanceService yahooFinanceService;

        /// <summary>
        /// The financial calculation service.
        /// </summary>
        private IFinancialCalculationService financialCalculationService;

        /// <summary>
        /// The google finance service.
        /// </summary>
        private IGoogleFinanceService googleFinanceService;

        /// <summary>
        /// The Portfolio Controller.
        /// </summary>
        private PortfolioController portfolioController;

        #endregion Fields

        [TestInitialize]
        public void Init()
        {
            AutoMapperConfig.Configure();

            yahooFinanceService = new YahooFinanceService();
            financialCalculationService = new FinancialCalculationService();
            googleFinanceService = new GoogleFinanceService(new WebRequestService());

            portfolioController = new PortfolioController(yahooFinanceService, googleFinanceService, financialCalculationService);
        }

        /// <summary>
        /// The option data test.
        /// </summary>
        [TestMethod]
        public void PortfolioController_DoesGetOptionData_FromService()
        {
            var data = portfolioController.OptionData("MSFT", null, null);
            Assert.IsTrue(data.Data != null);
        }

        /// <summary>
        /// The option data test.
        /// </summary>
        [TestMethod]
        public void PortfolioController_DoesGetAutocompleteData_FromService()
        {
            var data = portfolioController.AutoComplete("APPL");
            Assert.IsTrue(data.Data != null);
        }
    }
}