using System;
using System.Data.Entity;

namespace Core.ORM.Repositories
{
    public class EntityFrameWorkRepository<TEntity, TKey> where TEntity : class
    {
        private readonly DbContext dbContext;

        public EntityFrameWorkRepository(DbContext dbContext)
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext");
            dbContext = dbContext;
        }

        protected DbContext DbContext
        {
            get { return dbContext; }
        }

        public void Create(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            DbContext.Set<TEntity>().Add(entity);
        }

        public TEntity GetById(TKey id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            DbContext.Set<TEntity>().Attach(entity);
            DbContext.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            DbContext.Set<TEntity>().Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
