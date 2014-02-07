using System;
using System.Collections.Generic;
using System.Data;

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
        /// Select * for particular entity.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();
    }
}
