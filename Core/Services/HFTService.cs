using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Core.Models.HFT;
using Core.ORM;
using DapperExtensions;

namespace Core.Services
{
    public class HFTService<T> : ServiceBase<T> where T : class, new()
    {
        /// <summary>
        /// Gets tick data collection from db by matching symbol.
        /// </summary>
        /// <param name="symbol">The symbol to look for.</param>
        /// <returns>Collection of tick data.</returns>
        public static IEnumerable<Tick> BySymbol(string symbol)
        {
            using (var c = new SqlConnection(Constants.AlgoTradingDbConnectionStr))
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
