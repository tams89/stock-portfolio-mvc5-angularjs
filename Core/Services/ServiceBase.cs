// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceBase.cs" company="">
//   
// </copyright>
// <summary>
//   The service base.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



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
    /// <summary>
    /// The service base.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class ServiceBase<T> where T : class, new()
    {
        /// <summary>
        /// Gets the authenticated user.
        /// </summary>
        public string AuthenticatedUser
        {
            get
            {
                var currentIdent = WindowsIdentity.GetCurrent();
                return currentIdent != null ? currentIdent.Name : string.Empty;
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
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
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