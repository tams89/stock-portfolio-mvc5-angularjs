using System;
using System.Collections.Generic;
using System.Linq;
using AlgoTrader.Core.DTO;
using AlgoTrader.Core.Services.Interfaces;
using Newtonsoft.Json.Linq;

namespace AlgoTrader.Core.Services
{
    /// <summary>
    /// The google finance service.
    /// </summary>
    public class GoogleFinanceService : IGoogleFinanceService
    {
        /// <summary>
        /// The _web request service.
        /// </summary>
        private readonly IWebRequestService _webRequestService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleFinanceService"/> class. 
        /// Service that encapsulates the Google Finance API.
        /// </summary>
        /// <param name="webRequestService">
        /// </param>
        public GoogleFinanceService(IWebRequestService webRequestService)
        {
            this._webRequestService = webRequestService;
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
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        public IEnumerable<GoogleFinanceAutoCompleteDto> SymbolSearch(string term)
        {
            try
            {
                if (string.IsNullOrEmpty(term)) return Enumerable.Empty<GoogleFinanceAutoCompleteDto>();

                // Query string plus argument, returns json.
                var url = Constants.GoogleFinanceJsonApiUrl + term.Trim();

                // Download json data as a string.
                var json = _webRequestService.GetResponse(url);

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
                        new GoogleFinanceAutoCompleteDto
                        {
                            Symbol = x["t"].ToObject<string>(),
                            Name = x["n"].ToObject<string>()
                        })
                        .Where(x => !string.IsNullOrEmpty(x.Symbol) && !string.IsNullOrEmpty(x.Name));

                return jsonCustomArray;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}