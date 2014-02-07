using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace Core.Repository
{
    /// <summary>
    /// Read only contracts.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReadOnlyRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// Database connection.
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// Find entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T FindById(Guid id);

        /// <summary>
        /// Find by predicated member.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Select * for particular entity.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();
    }
}
