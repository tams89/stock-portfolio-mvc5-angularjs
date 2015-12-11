using System;
using System.Collections.Generic;
using System.Net;
using AlgoTrader.Core.DTO;
using AlgoTrader.Core.Services.Interfaces;

namespace AlgoTrader.Core.Services
{
    /// <summary>
    /// FinancialCalculationService contains methods to calculate properties of option & stocks.
    /// </summary>
    public class FinancialCalculationService : IFinancialCalculationService
    {
        /// <summary>
        /// Key value pair storing the symbol and volatility of a option.
        /// </summary>
        private KeyValuePair<string, double> _symbolVolatility;

        /// <summary>
        /// Calculates the Black-Scholes price of an option.
        /// High-Low volatility used with a range of one year, should be set in option already.
        /// </summary>
        /// <param name="option">
        /// </param>
        /// <returns>
        /// </returns>
        public double BlackScholes(OptionDto option)
        {
            try
            {
                if (Math.Abs(option.Volatility) < double.Epsilon)
                    return 0.0;

                var optionType = option.Symbol[10] == 'C' ? AlgoTrader.BlackScholes.Type.Call : AlgoTrader.BlackScholes.Type.Put;
                var timeToExpiryInYears = float.Parse(option.DaysToExpiry) / 365;
                var blackScholesPrice = AlgoTrader.BlackScholes.Black_Scholes(
                    optionType,
                    (double)option.LastPrice,
                    (double)option.StrikePrice,
                    timeToExpiryInYears,
                    0.25, // U.S. Govt. Treasury Interest Rate circa.12/2013.
                    option.Volatility);

                return Math.Round(blackScholesPrice, 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// The volatility.
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        /// <param name="fromDate">
        /// </param>
        /// <param name="toDate">
        /// The to Date.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public double Volatility(OptionDto option, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                var companyTicker = string.Empty;
                for (var i = 0; i <= 4; i++)
                {
                    int res;
                    var isNum = int.TryParse(option.Symbol[i].ToString(), out res);
                    if (!isNum)
                        companyTicker += option.Symbol[i];
                }

                // Annual volatilty by default.
                fromDate = fromDate ?? DateTime.Today.AddYears(-1);
                toDate = toDate ?? DateTime.Today;

                // If volatility already calculated for this symbol/option use the stored value.
                double volatility = 0;
                if (_symbolVolatility.Key != companyTicker)
                {
                    volatility = AlgoTrader.YahooApi.VolatilityAndMarketData.highLowVolatility(companyTicker, fromDate.Value, toDate.Value);
                    _symbolVolatility = new KeyValuePair<string, double>(companyTicker, volatility);
                }
                else if (_symbolVolatility.Key == companyTicker)
                    volatility = _symbolVolatility.Value;
                if (Math.Abs(volatility) < double.Epsilon)
                    throw new InvalidOperationException(
                        $"Volatility cannot be zero (tolerance double epsilson constant)'{volatility}'");

                return volatility;
            }
            catch (WebException ex)
            {
                // TODO Log http exception and carry on.
                return 0.0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}