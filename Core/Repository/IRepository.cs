using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AlgoTrader.Core.Repository
{
    /// <summary>
    /// Repository contracts.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> : IReadOnlyRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// Linq constructable query that is executed after full query is planned.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> AsQueryable();

        /// <summary>
        /// Loads entity into memory then performs search based on predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Insert entity.
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// Delete entity.
        /// </summary>
        /// <param name="entity"></param>
        void Remove(T entity);

        /// <summary>
        /// Attach an entity. (Entity Framework)
        /// </summary>
        /// <param name="entity"></param>
        void Attach(T entity);
    }
}
