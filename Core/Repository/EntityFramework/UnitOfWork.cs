using Core.Models.HFT;
using System.Data.Entity;

namespace Core.Repository.EntityFramework
{
    /// <summary>
    /// Entity Framework work unit.
    /// </summary>
    public class UnitOfWork : DbContext, IUnitOfWork
    {
        private readonly Repository<Tick> tickRepository;

        /// <summary>
        /// Tick database collection.
        /// </summary>
        public DbSet<Tick> Ticks { get; set; }

        /// <summary>
        /// Constructs a unit of work.
        /// </summary>
        public UnitOfWork()
        {
            tickRepository = new Repository<Tick>(Ticks);
        }

        /// <summary>
        /// The Tick Repository.
        /// </summary>
        public IRepository<Tick> TickRepository
        {
            get { return tickRepository; }
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
