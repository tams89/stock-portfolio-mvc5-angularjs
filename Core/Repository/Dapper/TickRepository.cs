using Core.Models.HFT;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Repository.Dapper
{
    /// <summary>
    /// Dapper Read-Only Tick repository.
    /// </summary>
    public class TickRepository : IReadOnlyRepository<Tick>
    {
        /// <summary>
        /// Database connections.
        /// </summary>
        public IDbConnection Connection
        {
            get { return new SqlConnection(Constants.AlgoTradingDbConnectionStr); }
        }

        /// <summary>
        /// Find Tick data by its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Tick FindById(Guid id)
        {
            Tick tick;

            using (var cn = Connection)
            {
                cn.Open();
                tick = cn.Get<Tick>(id);
            }

            return tick;
        }

        /// <summary>
        /// Get all Tick data.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Tick> GetAll()
        {
            IEnumerable<Tick> ticks;

            using (var cn = Connection)
            {
                cn.Open();
                ticks = cn.GetList<Tick>();
            }

            return ticks;
        }


        // TODO: Memory profile with where attached to query or seperate to initial query.
        /// <summary>
        /// Get all tick data by provided predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Tick> Find(Expression<Func<Tick, bool>> predicate)
        {
            IEnumerable<Tick> ticks;

            using (var cn = Connection)
            {
                cn.Open();
                ticks = cn.GetList<Tick>().Where(predicate.Compile());
            }

            return ticks;
        }
    }
}
