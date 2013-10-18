using Core.Models.HFT;
using Core.ORM;
using DapperExtensions;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Core.Models
{
    public abstract class EntityBase<T> where T : class, new()
    {
        private const string ConnectionString = Constants.AlgoTradingDbConnectionStr;

        /// <summary>
        /// Gets all data pertaining to the specified type.
        /// </summary>
        /// <returns>Collection of tick data.</returns>
        public static IEnumerable<T> Get()
        {
            using (var c = new SqlConnection(ConnectionString))
            {
                c.Open();
                var entity = c.GetList<T>(buffered: true);
                c.Close();

                return entity;
            }
        }
    }
}