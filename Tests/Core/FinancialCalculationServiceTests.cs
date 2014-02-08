using Core.DTO;
using Core.Exceptions;
using Core.Services;
using Core.Services.Interfaces;
using NUnit.Framework;

namespace Test.Core
{
    [TestFixture]
    public class FinancialCalculationServiceTests
    {
        private IFinancialCalculationService financialCalculationService;

        [SetUp]
        public void Init()
        {
            financialCalculationService = new FinancialCalculationService();
        }

        [Test]
        public void VolatilityFromSymbol()
        {
            var volatility = financialCalculationService.Volatility(new OptionDto { Symbol = "GOOG7140214C01175000" }, null, null);
            Assert.IsTrue(volatility * 100 > 1);
        }

        [Test]
        [ExpectedException(typeof(InvalidSymbolException))]
        public void Volatility_Exception_InvalidSymbol()
        {
            financialCalculationService.Volatility(new OptionDto { Symbol = "trlkhenriu" }, null, null);
        }
    }
}
