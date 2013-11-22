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
    using System;
    using System.Collections.Generic;
    using DTO;
    using Models.Site;

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
        IEnumerable<MarketDto> GetData(string symbol, DateTime? @from, DateTime? to);

        /// <summary>
        /// Obtains option table by symbol.
        /// </summary>
        /// <param name="symbol">
        /// The symbol.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<OptionDto> GetData(string symbol);

        /// <example>
        /// {"matches":a[{"t":"MSFT","n":"Microsoft Corporation","e":"NASDAQ","id":"358464"},
        /// {"t":"AMD","n":"Advanced Micro Devices, Inc.","e":"NYSE","id":"327"},
        /// {"t":"MU","n":"Micron Technology, Inc.","e":"NASDAQ","id":"12441984"}...
        /// </example>
        /// <summary>
        /// Searches API for matching companies by symbol or company name.
        /// </summary>
        /// <param name="term">
        /// The term.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<GoogleFinanceJsonDto> SymbolSearch(string term);

        #endregion
    }
}