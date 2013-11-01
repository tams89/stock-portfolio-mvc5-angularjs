using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AlgoTrader.YahooApi;
using Core.DTOs;
using Core.Factory;
using Core.Services.Interfaces;
using Core.Utilities;
using Newtonsoft.Json.Linq;
using Omu.ValueInjecter;

namespace Core.Services
{
    public class YahooFinanceService : IYahooFinanceService
    {
        public struct GoogleFinanceJSON
        {
            public string Symbol { get; set; }
            public string Name { get; set; }
        }

        /// <example>
        /// {"matches":a[{"t":"MSFT","n":"Microsoft Corporation","e":"NASDAQ","id":"358464"},
        /// {"t":"AMD","n":"Advanced Micro Devices, Inc.","e":"NYSE","id":"327"},
        /// {"t":"MU","n":"Micron Technology, Inc.","e":"NASDAQ","id":"12441984"}...
        /// </example>
        /// <summary>
        /// Searches API for matching companies by symbol or company name.
        /// </summary>
        public IEnumerable<GoogleFinanceJSON> SymbolSearch(string term)
        {
            try
            {
                if (string.IsNullOrEmpty(term)) return Enumerable.Empty<GoogleFinanceJSON>();

                WebRequest.DefaultWebProxy = null;
                using (var client = new WebClient())
                {
                    // Query string plus argument, returns json string.
                    var googleFinanceJson = @"http://www.google.com/finance/match?matchtype=matchall&ei=zhbaUIDlCKSWiAL8zwE&q=" + term.Trim();

                    // Download json data as a string.
                    var json = client.DownloadString(googleFinanceJson);

                    // Useful data in Json contained within [...]
                    var firstOccurrence = json.IndexOf('[');
                    var secondOccurrence = json.IndexOf(']');

                    // Remove invalid chars from string perfore parsing.
                    var trimmed = json.Substring(firstOccurrence, secondOccurrence - firstOccurrence + 1);

                    // Parse json array to valid json object.
                    var parsed = JArray.Parse(trimmed);

                    // This array can be customised to give the desired JSON array.
                    var jsonCustomArray = parsed
                        .Select(x => new GoogleFinanceJSON
                        {
                            Symbol = x["t"].ToObject<string>(),
                            Name = x["n"].ToObject<string>()
                        })
                        .Where(x => !string.IsNullOrEmpty(x.Symbol) && !string.IsNullOrEmpty(x.Name));

                    return jsonCustomArray;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtains stock data related to symbol.
        /// </summary>
        public IEnumerable<MarketDto> GetData(string symbol, DateTime? from, DateTime? to)
        {
            if (string.IsNullOrEmpty(symbol)) return null;
            if (!from.HasValue) from = DateTime.Now.AddMonths(-3);
            if (!to.HasValue) to = DateTime.Now;
            var marketData = VolatilityAndMarketData.getMarketData(symbol, from.Value, to.Value).ToList();
            return DtoInjector<VolatilityAndMarketData.MarketData, MarketDto>.InjectList(marketData);
        }

        /// <summary>
        /// Obtains option table by symbol.
        /// </summary>
        public IEnumerable<OptionDto> GetData(string symbol)
        {
            if (string.IsNullOrEmpty(symbol)) return null;
            var optionData = Options.GetOptionsData(symbol);
            return DtoInjector<Options.OptionsData, OptionDto>.InjectList(optionData);
        }
    }
}