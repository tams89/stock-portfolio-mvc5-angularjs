using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Core.Repository.EntityFramework
{
    /// <summary>
    /// Entity Framework Respository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class,IEntity
    {
        private readonly DbSet<T> dbSet;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbSet"></param>
        public Repository(DbSet<T> dbSet)
        {
            this.dbSet = dbSet;
        }

        public IQueryable<T> AsQueryable()
        {
            return dbSet.AsQueryable();
        }

        public IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Attach(T entity)
        {
            dbSet.Attach(entity);
        }

        public IDbConnection Connection
        {
            get { throw new System.NotImplementedException(); }
        }

        public T FindById(Guid id)
        {
            return dbSet.Single(i => i.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet;
        }
    }
}
