using System;
using AlgoTrader.Core.Models.HFT;

namespace AlgoTrader.Core.Repository
{
    /// <summary>
    /// Work contracts.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Repository containing Tick data.
        /// </summary>
        IRepository<Tick> TickRepository { get; }

        /// <summary>
        /// Save any changes.
        /// </summary>
        void Save();
    }
}
