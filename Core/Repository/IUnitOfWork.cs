using Core.EntityFramework;
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
        IRepository<Portfolio> PortfolioRepository { get; }

        /// <summary>
        /// Save any changes.
        /// </summary>
        void Save();
    }
}
