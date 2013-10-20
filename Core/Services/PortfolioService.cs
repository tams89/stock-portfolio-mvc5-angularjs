using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Portfolio;
using Core.ORM;
using Dapper;
using DapperExtensions;

namespace Core.Services
{
    public class PortfolioService<T> : ServiceBase<T> where T : class, new()
    {
        /// <summary>
        /// Get a list of all the securities the current users portfolio contains.
        /// </summary>
        public IEnumerable<Security> SecurityList()
        {
            using (var c = new SqlConnection(Constants.AlgoTradingDbConnectionStr))
            {
                c.Open();
                var symbols = c.Query<Security>
                    (
                        "Portfolio.SelectSymbolsByUser", 
                        new { UserName = AuthenticatedUser },
                        commandType: CommandType.StoredProcedure
                    );
                c.Close();
                return symbols;
            }
        }
    }
}