using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ORM;
using DapperExtensions;

namespace Core.Services
{
    public class ServiceBase<T> where T : class, new()
    {
        /// <summary>
        /// Gets all data pertaining to the specified type.
        /// </summary>
        /// <returns>Collection of tick data.</returns>
        public IEnumerable<T> GetAll()
        {
            using (var c = new SqlConnection(Constants.AlgoTradingDbConnectionStr))
            {
                c.Open();
                var entity = c.GetList<T>(buffered: true);
                c.Close();

                return entity;
            }
        }
    }
}
