using System.Collections.Generic;
using Core.Models.HFT;

namespace Core.Services.Interfaces
{
    public interface IHFTService
    {
        /// <summary>
        /// Gets tick data collection from db by matching symbol.
        /// </summary>
        /// <param name="symbol">The symbol to look for.</param>
        /// <returns>Collection of tick data.</returns>
        IEnumerable<Tick> BySymbol(string symbol);
    }
}