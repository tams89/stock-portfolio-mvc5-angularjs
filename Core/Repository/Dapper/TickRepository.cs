using Core.Models.HFT;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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
        public System.Data.IDbConnection Connection
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
    }
}
