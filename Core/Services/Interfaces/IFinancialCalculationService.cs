namespace Core.Services.Interfaces
{
    using DTO;
    using System;

    /// <summary>
    /// Interface to FinancialCalculationService contains methods to calculate properties of option & stocks.
    /// </summary>
    public interface IFinancialCalculationService
    {
        /// <summary>
        /// Calculates the Black-Scholes price of an option.
        /// High-Low volatility used with a range of one year if no range specified explicity.
        /// </summary>
        /// <param name="option">
        /// </param>
        /// <returns>
        /// </returns>
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
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        double Volatility(OptionDto option, DateTime? fromDate, DateTime? toDate);
    }
}