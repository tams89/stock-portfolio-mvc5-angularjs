using System.Collections.Generic;
using System.Linq;
using AlgoTrader.Core.Models.HFT;
using AlgoTrader.Core.Repository;
using AlgoTrader.Core.Services.Interfaces;

namespace AlgoTrader.Core.Services
{
    /// <summary>
    /// The hft service.
    /// </summary>
    public class HFTService : IHFTService
    {
        private readonly IReadOnlyRepository<Tick> tickRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tickRepository"></param>
        public HFTService(IReadOnlyRepository<Tick> tickRepository)
        {
            this.tickRepository = tickRepository;
        }

        /// <summary>
        /// Gets tick data collection from db by matching symbol.
        /// </summary>
        /// <param name="symbol">
        /// The symbol to look for.
        /// </param>
        /// <returns>
        /// Collection of tick data.
        /// </returns>
        public IEnumerable<Tick> BySymbol(string symbol)
        {
            var tickData = string.IsNullOrEmpty(symbol) ?
                Enumerable.Empty<Tick>() : tickRepository.Find(x => x.Symbol.Contains(symbol));

            return tickData;
        }
    }
}
