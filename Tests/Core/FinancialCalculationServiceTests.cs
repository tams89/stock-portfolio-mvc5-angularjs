using Core.DTO;
using Core.Exceptions;
using Core.Services;
using Core.Services.Interfaces;
using NUnit.Framework;
using System;

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
        public void VolatilityWithDate()
        {
            var volatilityDate = financialCalculationService.Volatility(new OptionDto { Symbol = "GOOG7140214C01175000" }, DateTime.Today.AddDays(-50), DateTime.Today);
            Assert.IsTrue(volatilityDate > double.Epsilon);
        }

        [Test]
        [ExpectedException(typeof(InvalidSymbolException))]
        public void Volatility_Exception_InvalidSymbol()
        {
            financialCalculationService.Volatility(new OptionDto { Symbol = "trlkhenriu" }, null, null);
        }

        [Test]
        public void Volatility_Exception()
        {
            var optionDto = new OptionDto();
            Assert.Catch<Exception>(() => financialCalculationService.Volatility(optionDto, null, null));
        }

        [Test]
        public void BlackScholes_Exception()
        {
            var optionDto = new OptionDto { Volatility = float.Epsilon };
            Assert.Catch<Exception>(() => financialCalculationService.BlackScholes(optionDto));
        }

        /// <summary>
        /// For Black Scholes 
        /// Requires: optionType, LastPrice, StrikePrice, DaysToExpiry, IntRate, Volatility
        /// </summary>
        private static OptionDto[] optionDtos =
        {
            new OptionDto {}
        };
    }
}
