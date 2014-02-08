using Core.EntityFramework;
using System.Data.Entity;

namespace Core.Repository.EntityFramework
{
    /// <summary>
    /// Entity Framework work unit.
    /// </summary>
    public class UnitOfWork : DbContext, IUnitOfWork
    {
        private readonly Repository<Portfolio> portfolioRepository;

        /// <summary>
        /// Tick database collection.
        /// </summary>
        private DbSet<Portfolio> Portfolios { get; set; }

        /// <summary>
        /// Constructs a unit of work.
        /// </summary>
        public UnitOfWork()
        {
            portfolioRepository = new Repository<Portfolio>(Portfolios);
        }

        /// <summary>
        /// The Tick Repository.
        /// </summary>
        public IRepository<Portfolio> PortfolioRepository
        {
            get { return portfolioRepository; }
        }

        /// <summary>
        /// Save any changes.
        /// </summary>
        public void Save()
        {
            SaveChanges();
        }
    }
}
