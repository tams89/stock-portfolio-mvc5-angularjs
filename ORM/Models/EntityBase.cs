using DapperExtensions;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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
    }
}