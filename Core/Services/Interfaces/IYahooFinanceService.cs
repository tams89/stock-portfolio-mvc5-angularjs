using System;
using System.Collections.Generic;
using AlgoTrader.Core.DTO;

namespace AlgoTrader.Core.Services.Interfaces
{
    /// <summary>
    /// The YahooFinanceService interface.
    /// </summary>
    public interface IYahooFinanceService
    {
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
        IEnumerable<MarketDto> GetStockData(string symbol, DateTime? from = null, DateTime? to = null);

        /// <summary>
        /// The get option data.
        /// </summary>
        /// <param name="symbol">
        /// The symbol.
        /// </param>
        IEnumerable<OptionDto> GetOptionData(string symbol);
    }
}