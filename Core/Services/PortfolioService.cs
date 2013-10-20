using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Portfolio;
using Core.ORM;
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
                var sql = @"SELECT S.Symbol 
                            FROM Portfolio.[User] U
                            INNER JOIN Portfolio.Portfolio P ON U.UserId = P.UserId
                            INNER JOIN Portfolio.Portfolio_Security PS ON P.PortfolioId = PS.PortfolioId
                            INNER JOIN Portfolio.Security S ON PS.SecurityId = S.SecurityId";
                c.Close();

                return entity;
            }
        }
   }
}