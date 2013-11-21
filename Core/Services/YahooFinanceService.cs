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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using AlgoTrader.YahooApi;

    using Core.DTO;
    using Core.Services.Interfaces;
    using Core.Utilities;

    using Newtonsoft.Json.Linq;

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
            if (string.IsNullOrEmpty(symbol))
            {
                return null;
            }

            if (!from.HasValue)
            {
                from = DateTime.Today.AddYears(-2);
            }

            if (!to.HasValue)
            {
                to = DateTime.Today;
            }

            var marketData = VolatilityAndMarketData.getMarketData(symbol, from.Value, to.Value).Reverse();
                
                // old to recent.
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
            if (string.IsNullOrEmpty(symbol))
            {
                return null;
            }

            var optionData = Options.GetOptionsData(symbol);
            return DtoInjector<Options.OptionsData, OptionDto>.InjectList(optionData);
        }

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
        public IEnumerable<GoogleFinanceJSON> SymbolSearch(string term)
        {
            try
            {
                if (string.IsNullOrEmpty(term))
                {
                    return Enumerable.Empty<GoogleFinanceJSON>();
                }

                WebRequest.DefaultWebProxy = null;
                using (var client = new WebClient())
                {
                    // Query string plus argument, returns json string.
                    var googleFinanceJson =
                        @"http://www.google.com/finance/match?matchtype=matchall&ei=zhbaUIDlCKSWiAL8zwE&q="
                        + term.Trim();

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
                    var jsonCustomArray =
                        parsed.Select(
                            x =>
                            new GoogleFinanceJSON
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

        #endregion

        /// <summary>
        /// The google finance json.
        /// </summary>
        public struct GoogleFinanceJSON
        {
            #region Public Properties

            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the symbol.
            /// </summary>
            public string Symbol { get; set; }

            #endregion
        }
    }
}