// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IYahooFinanceService.cs" company="">
//   
// </copyright>
// <summary>
//   The YahooFinanceService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Core.Services.Interfaces
{
    using DTO;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The YahooFinanceService interface.
    /// </summary>
    public interface IYahooFinanceService
    {
        #region Public Methods and Operators

        /// <summary>
        /// Obtains market data related to symbol.
        /// </summary>
        /// <param name="symbol">
        /// The symbol.
        /// </param>
        /// <param name="from">
        /// The from.
        /// </param>
        /// <param name="to">
        /// The to.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<MarketDto> GetStockData(string symbol, DateTime? @from, DateTime? to);

        /// <summary>
        /// The get option data.
        /// </summary>
        /// <param name="symbol">
        /// The symbol.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<OptionDto> GetOptionData(string symbol);

        #endregion
    }
}