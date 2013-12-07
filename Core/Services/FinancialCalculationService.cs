
namespace Core.Services
{
    using Core.DTO;
    using Core.Services.Interfaces;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// FinancialCalculationService contains methods to calculate properties of option & stocks.
    /// </summary>
    public class FinancialCalculationService : IFinancialCalculationService
    {
        /// <summary>
        /// Key value pair storing the symbol and volatility of a option.
        /// </summary>
        private KeyValuePair<string, double> symbolVolatility;

        /// <summary>
        /// Calculates the Black-Scholes price of an option.
        /// High-Low volatility used with a range of one year if no range specified explicity.
        /// </summary>
        /// <param name="option"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public double BlackScholes(OptionDto option, DateTime? fromDate, DateTime? toDate)
        {
            // Annual volatilty used as a result of these defaults.
            if (!fromDate.HasValue || !toDate.HasValue)
            {
                fromDate = DateTime.Today.AddYears(-1);
                toDate = DateTime.Today;
            }

            try
            {
                // If volatility already calculated for this symbol/option use the stored value.
                double volatility = 0;
                if (symbolVolatility.Key != option.Symbol)
                {
                    volatility = AlgoTrader.YahooApi.VolatilityAndMarketData.highLowVolatility(option.Symbol.Substring(0, 4), fromDate.Value, toDate.Value);
                    symbolVolatility = new KeyValuePair<string, double>(option.Symbol, volatility);
                }

                if (symbolVolatility.Key == option.Symbol) volatility = symbolVolatility.Value;
                if (Math.Abs(volatility) < double.Epsilon) throw new InvalidOperationException(string.Format("Volatility cannot be zero (tolerance double epsilson constant)'{0}'", volatility));

                var optionType = option.Symbol[10] == 'C' ? AlgoTrader.BlackScholes.Type.Call : AlgoTrader.BlackScholes.Type.Put;
                var timeToExpiryInYears = float.Parse(option.DaysToExpiry) / 365;
                var blackScholesPrice = AlgoTrader.BlackScholes.Black_Scholes(
                    optionType,
                    (double)option.LastPrice,
                    (double)option.StrikePrice,
                    timeToExpiryInYears,
                    0.25, // U.S. Govt. Treasury Interest Rate circa.12/2013.
                    volatility);

                return blackScholesPrice;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
