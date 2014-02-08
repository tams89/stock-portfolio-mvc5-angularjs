using Core.Models.HFT;
using Core.Repository;
using Core.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services
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

        /// <summary>
        /// Retrieves all ticks in DB.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Tick> AllTicks()
        {
            return tickRepository.GetAll();
        }
    }
}
