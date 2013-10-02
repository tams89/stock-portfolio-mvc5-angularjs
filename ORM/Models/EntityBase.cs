using DapperExtensions;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Models
{
    public abstract class EntityBase<T> where T : class
    {
        private static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["HFTDB"].ConnectionString;
            }
        }

        public static T Get(int? id = null)
        {
            using (var c = new SqlConnection(ConnectionString))
            {
                c.Open();
                var entity = c.Get<T>(id);
                c.Close();

                return entity;
            }
        }
    }
}