using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace AlgoTrader.Core.Repository.EntityFramework
{
    /// <summary>
    /// Entity Framework Respository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class, IEntity
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> AsQueryable()
        {
            return dbSet.AsQueryable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Attach(T entity)
        {
            dbSet.Attach(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        public IDbConnection Connection
        {
            get { throw new System.NotImplementedException(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T FindById(Guid id)
        {
            return dbSet.Single(i => i.Id == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            return dbSet;
        }
    }
}
