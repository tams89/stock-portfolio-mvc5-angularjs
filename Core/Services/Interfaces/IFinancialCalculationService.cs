namespace Core.Services.Interfaces
{
    using Core.DTO;
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
        /// <param name="option"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        double BlackScholes(OptionDto option, DateTime? fromDate, DateTime? toDate);
    }
}