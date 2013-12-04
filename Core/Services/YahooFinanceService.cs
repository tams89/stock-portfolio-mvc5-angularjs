// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YahooFinanceService.cs" company="">
//   
// </copyright>
// <summary>
//   The yahoo finance service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Core.Services
{
    using AlgoTrader.YahooApi;
    using DTO;
    using Interfaces;
    using Microsoft.FSharp.Collections;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities;

    /// <summary>
    /// The yahoo finance service.
    /// </summary>
    public class YahooFinanceService : IYahooFinanceService
    {
        #region Public Methods and Operators

        /// <summary>
        /// Obtains stock data related to symbol.
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
        public IEnumerable<MarketDto> GetData(string symbol, DateTime? from, DateTime? to)
        {
            if (string.IsNullOrEmpty(symbol)) return Enumerable.Empty<MarketDto>();
            if (!from.HasValue) from = DateTime.Today.AddYears(-2);
            if (!to.HasValue) to = DateTime.Today;
            var marketData = VolatilityAndMarketData.getMarketData(symbol, from.Value, to.Value).Reverse(); // Old to new.
            return DtoInjector<VolatilityAndMarketData.MarketData, MarketDto>.InjectList(marketData);
        }

        /// <summary>
        /// Obtains option table by symbol.
        /// </summary>
        /// <param name="symbol">
        /// The symbol.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<OptionDto> GetData(string symbol)
        {
            var optionData = !string.IsNullOrEmpty(symbol) ? Options.GetOptionsData(symbol) : FSharpList<Options.OptionsData>.Empty;
            return DtoInjector<Options.OptionsData, OptionDto>.InjectList(optionData);
        }

        #endregion
    }
}