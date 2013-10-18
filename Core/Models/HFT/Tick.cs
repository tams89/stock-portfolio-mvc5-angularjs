using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Core.ORM;
using DapperExtensions;

namespace Core.Models.HFT
{
    /// <summary>
    /// Tick Model
    /// </summary>
    public class Tick : EntityBase<Tick>
    {
        public Guid Id { get; set; }

        public string Symbol { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        public decimal Open { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Close { get; set; }

        public int Volume { get; set; }

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