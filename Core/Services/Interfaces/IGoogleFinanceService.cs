// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGoogleFinanceService.cs" company="">
//   
// </copyright>
// <summary>
//   Interface to google finance service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


using System.Collections.Generic;
using Core.Models.Site;

namespace Core.Services.Interfaces
{
    /// <summary>
    /// Interface to google finance service.
    /// </summary>
    public interface IGoogleFinanceService
    {
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
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        IEnumerable<GoogleFinanceAutoCompleteDto> SymbolSearch(string term);
    }
}