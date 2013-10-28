using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AlgoTrader.YahooApi;
using Core.Services.Interfaces;
using Newtonsoft.Json.Linq;

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

        /// <summary>
        /// Obtains market data related to symbol.
        /// </summary>
        public VolatilityAndMarketData.MarketData GetMarketData(string symbol, DateTime? from, DateTime? to)
        {
            if (string.IsNullOrEmpty(symbol)) return null;

            if (!from.HasValue)
                from = DateTime.Now.AddMonths(-3);
            if (!to.HasValue)
                to = DateTime.Now;

            var marketData = VolatilityAndMarketData.getMarketData(symbol, from.Value, to.Value);
            return marketData.Head;
        }
    }
}