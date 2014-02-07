using Core.Models.HFT;
using System.Collections.Generic;

namespace Core.Services.Interfaces
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