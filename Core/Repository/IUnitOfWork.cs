using Core.Models.HFT;
using System;

namespace Core.Repository
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
