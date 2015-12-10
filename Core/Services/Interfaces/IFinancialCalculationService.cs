using System;
using AlgoTrader.Core.DTO;

namespace AlgoTrader.Core.Services.Interfaces
{
    /// <summary>
    /// Interface to FinancialCalculationService contains methods to calculate properties of option and stocks.
    /// </summary>
    public interface IFinancialCalculationService
    {
        /// <summary>
        /// Calculates the Black-Scholes price of an option.
        /// High-Low volatility used with a range of one year if no range specified explicity.
        /// </summary>
        /// <param name="option">
        /// </param>
        double BlackScholes(OptionDto option);

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
        double Volatility(OptionDto option, DateTime? fromDate = null, DateTime? toDate = null);
    }
}