using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using AlgoTrader.Core.DTO;
using AlgoTrader.Core.Services.Interfaces;
using AlgoTrader.YahooApi;
using AutoMapper;

namespace AlgoTrader.Core.Services
{
    public class YahooFinanceService : IYahooFinanceService
    {
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
        public IEnumerable<MarketDto> GetStockData(string symbol, DateTime? from, DateTime? to)
        {
            if (string.IsNullOrEmpty(symbol))
                return Enumerable.Empty<MarketDto>();
            if (from.HasValue && to.HasValue && (to.Value > from.Value))
                throw new InvalidDataException("'To' date must be after 'From' date.");
            if (!from.HasValue)
                from = DateTime.Today.AddYears(-2);
            if (!to.HasValue)
                to = DateTime.Today;

            var marketData = Enumerable.Empty<VolatilityAndMarketData.MarketData>();

            try
            {
                // Old to new.
                marketData = VolatilityAndMarketData.getMarketData(symbol, from.Value, to.Value).Reverse();
            }
            catch (WebException ex)
            {
                // means information for symbol not found so allow execution to continue and return empty collection.
                if (!ex.Message.Contains("404")) throw ex;
            }

            return Mapper.Map<IEnumerable<VolatilityAndMarketData.MarketData>, IEnumerable<MarketDto>>(marketData);
        }

        /// <summary>
        /// Obtains option table by symbol.
        /// </summary>
        /// <param name="symbol">
        /// The symbol.
        /// </param>
        [Obsolete("YQL Options API no longer funtional.")]
        public IEnumerable<OptionDto> GetOptionData(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
                return Enumerable.Empty<OptionDto>();
            var optionData = Options.GetOptionsData(symbol);
            return Mapper.Map<IEnumerable<Options.OptionsData>, IEnumerable<OptionDto>>(optionData);
        }
    }
}