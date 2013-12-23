
namespace Core.Services
{
    using AlgoTrader.YahooApi;
    using AutoMapper;
    using DTO;
    using Interfaces;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

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
        public IEnumerable<MarketDto> GetStockData(string symbol, DateTime? from, DateTime? to)
        {
            if (string.IsNullOrEmpty(symbol))
                return Enumerable.Empty<MarketDto>();
            if (!from.HasValue)
                from = DateTime.Today.AddYears(-2);
            if (!to.HasValue)
                to = DateTime.Today;
            var marketData = VolatilityAndMarketData.getMarketData(symbol, from.Value, to.Value).Reverse(); // Old to new.
            return Mapper.Map<IEnumerable<VolatilityAndMarketData.MarketData>, IEnumerable<MarketDto>>(marketData);
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
        public IEnumerable<OptionDto> GetOptionData(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
                return Enumerable.Empty<OptionDto>();
            var optionData = Options.GetOptionsData(symbol);
            return Mapper.Map<IEnumerable<Options.OptionsData>, IEnumerable<OptionDto>>(optionData);
        }

        #endregion
    }
}