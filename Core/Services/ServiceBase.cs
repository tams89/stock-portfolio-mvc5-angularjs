using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Core.ORM;
using DapperExtensions;

namespace Core.Services
{
    public class ServiceBase<T> where T : class, new()
    {
        public string AuthenticatedUser
        {
            get
            {
                var currentIdent = WindowsIdentity.GetCurrent();
                return currentIdent != null ? currentIdent.Name : "";
            }
        }

        /// <summary>
        /// Gets all data.
        /// </summary>
        /// <returns>Collection of tick data.</returns>
        public IEnumerable<T> Get()
        {
            using (var c = new SqlConnection(Constants.AlgoTradingDbConnectionStr))
            {
                c.Open();
                var entity = c.GetList<T>(buffered: true);
                c.Close();
                return entity;
            }
        }

        /// <summary>
        /// Gets all data by id.
        /// </summary>
        public T Find(Guid id)
        {
            using (var c = new SqlConnection(Constants.AlgoTradingDbConnectionStr))
            {
                c.Open();
                var entity = c.Get<T>(id);
                c.Close();
                return entity;
            }
        }
    }
}