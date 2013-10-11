using Core.Models.HFT;
using Core.ORM;
using DapperExtensions;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Core.Models
{
    public abstract class EntityBase<T> where T : class
    {
        private const string ConnectionString = Constants.AlgoTradingDbConnectionStr;

        /// <summary>
        /// Gets all data pertaining to the specified type.
        /// </summary>
        /// <returns>Collection of tick data.</returns>
        public static IEnumerable<T> GetAll()
        {
            using (var c = new SqlConnection(ConnectionString))
            {
                c.Open();
                var entity = c.GetList<T>(buffered: true);
                c.Close();

                return entity;
            }
        }

        /// <summary>
        /// Gets tick data collection from db by matching symbol.
        /// </summary>
        /// <param name="symbol">The symbol to look for.</param>
        /// <returns>Collection of tick data.</returns>
        public static IEnumerable<Tick> GetBySymbol(string symbol)
        {
            using (var c = new SqlConnection(ConnectionString))
            {
                c.Open();
                var predicate = Predicates.Field<Tick>(x => x.Symbol, Operator.Like, symbol);
                var entity = c.GetList<Tick>(predicate);
                c.Close();

                return entity;
            }
        }
    }
}