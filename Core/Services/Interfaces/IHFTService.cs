using System.Collections.Generic;
using AlgoTrader.Core.Models.HFT;

namespace AlgoTrader.Core.Services.Interfaces
{
    /// <summary>
    /// The HFTService interface.
    /// </summary>
    public interface IHFTService
    {
        /// <summary>
        /// Gets tick data collection from db by matching symbol.
        /// </summary>
        /// <param name="symbol">
        /// The symbol to look for.
        /// </param>
        /// <returns>
        /// Collection of tick data.
        /// </returns>
        IEnumerable<Tick> BySymbol(string symbol);
    }
}