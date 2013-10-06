using DapperExtensions;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ORM.Models
{
    public abstract class EntityBase<T> where T : class
    {
        /// <summary>
        /// Retrieve the first connection string in the app.config.
        /// </summary>
        private static string _connectionString = "Data Source=.;Initial Catalog=HFT;Integrated Security=True";

        public static IEnumerable<T> GetAll()
        {
            using (var c = new SqlConnection(_connectionString))
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
            using (var c = new SqlConnection(_connectionString))
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