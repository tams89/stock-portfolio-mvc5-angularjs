using AlgoTrader.YahooApi;
using Core;
using Core.DTO;
using NUnit.Framework;
using System;

namespace Test.FinanceLibrary
{
    [TestFixture]
    public class AmericanOptions
    {
        private static readonly OptionDto[] optionDtos =
        {
            //new OptionDto { LastPrice = , StrikePrice = , DaysToExpiry = , Volatility = }
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
    }
}
