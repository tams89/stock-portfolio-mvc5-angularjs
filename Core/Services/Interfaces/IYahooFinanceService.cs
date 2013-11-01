using System;
using System.Collections.Generic;
using AlgoTrader.YahooApi;
using Core.DTOs;

namespace Core.Services.Interfaces
{
    public interface IYahooFinanceService
    {
        /// <example>
        /// {"matches":a[{"t":"MSFT","n":"Microsoft Corporation","e":"NASDAQ","id":"358464"},
        /// {"t":"AMD","n":"Advanced Micro Devices, Inc.","e":"NYSE","id":"327"},
        /// {"t":"MU","n":"Micron Technology, Inc.","e":"NASDAQ","id":"12441984"}...
        /// </example>
        /// <summary>
        /// Searches API for matching companies by symbol or company name.
        /// </summary>
        IEnumerable<YahooFinanceService.GoogleFinanceJSON> SymbolSearch(string term);

        /// <summary>
        /// Obtains market data related to symbol.
        /// </summary>
        IEnumerable<MarketDto> GetData(string symbol, DateTime? @from, DateTime? to);

        /// <summary>
        /// Obtains option table by symbol.
        /// </summary>
        IEnumerable<OptionDto> GetData(string symbol);
    }
}