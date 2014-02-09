using AlgoTrader.YahooApi;
using Core;
using Core.DTO;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net;

namespace Test.FinanceLibrary
{
    [TestFixture]
    public class FinanceCalcTests
    {
        private static readonly OptionDto[] optionDtos =
        {
            //new OptionDto { LastPrice = , StrikePrice = , DaysToExpiry = , Volatility = }
            // example PUT option.
            new OptionDto { Symbol = "MSFT140222P00037000", LastPrice = 0.97M, StrikePrice = 97.00M, DaysToExpiry = "14.0" }
        };

        [Test, TestCaseSource("optionDtos")]
        public void AmericanOptionBinomial(OptionDto dto)
        {
            var binomialPrice = AlgoTrader.AmericanOptionBinomial.americanPut
                ((double.Parse(dto.DaysToExpiry) / 365.0),
                 (double)dto.LastPrice,
                 (double)dto.StrikePrice,
                 2,
                 Constants.FedIntRate,
                 VolatilityAndMarketData.highLowVolatility("MSFT", DateTime.Today.AddYears(-1), DateTime.Today),
                 (2.4 / 100)); // dividend yield = annual dividends per share / price per share

            Assert.IsNotNull(binomialPrice);
        }

        [TestCase("MSFT", true)]
        [TestCase("GOOG", true)]
        [TestCase("", false)]
        public void GetOptionData(string symbol, bool shouldPass)
        {
            var optionData = Options.GetOptionsData(symbol);
            Assert.IsTrue(optionData.Any() == shouldPass);
        }

        [TestCase("MSFT")]
        [TestCase("GOOG")]
        public void GetHighLowVolatility(string symbol)
        {
            var volatility = VolatilityAndMarketData.highLowVolatility(symbol, DateTime.Today.AddYears(-1), DateTime.Today);
            Assert.IsTrue(volatility > double.Epsilon);
        }

        [TestCase("")]
        public void HighLowVolatilityNotFound(string symbol)
        {
            Assert.Catch<WebException>(() => VolatilityAndMarketData.highLowVolatility(symbol, DateTime.Today.AddYears(-1), DateTime.Today));
        }

        [TestCase("MSFT")]
        [TestCase("GOOG")]
        public void GetHighLowCloseVolatility(string symbol)
        {
            var volatility = VolatilityAndMarketData.highLowCloseVolatility(symbol, DateTime.Today.AddYears(-1), DateTime.Today);
            Assert.IsTrue(volatility > double.Epsilon);
        }

        [TestCase("")]
        public void HighLowCloseVolatilityNotFound(string symbol)
        {
            Assert.Catch<WebException>(() => VolatilityAndMarketData.highLowCloseVolatility(symbol, DateTime.Today.AddYears(-1), DateTime.Today));
        }

        [TestCase("MSFT")]
        [TestCase("GOOG")]
        public void GetMarketData(string symbol)
        {
            var data = VolatilityAndMarketData.getMarketData(symbol, DateTime.Today.AddYears(-1), DateTime.Today);
            Assert.IsTrue(data.Any());
        }

        [TestCase("")]
        public void MarketDataNotFound(string symbol)
        {
            Assert.Catch<WebException>(() => VolatilityAndMarketData.getMarketData(symbol, DateTime.Today.AddYears(-1), DateTime.Today));
        }

        [TestCase("MSFT")]
        [TestCase("GOOG")]
        public void GetStockData(string symbol)
        {
            var data = Stock.GetStocksData(symbol);
            var areNotEmptyOrNull = !string.IsNullOrEmpty(data.Ask) && !string.IsNullOrEmpty(data.Bid) &&
                                    !string.IsNullOrEmpty(data.Change) && !string.IsNullOrEmpty(data.Open) &&
                                    !string.IsNullOrEmpty(data.PreviousClose);

            Assert.IsTrue(areNotEmptyOrNull);
        }
    }
}
