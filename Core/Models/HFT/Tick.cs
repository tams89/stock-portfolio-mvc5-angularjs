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
    public class Tick
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
    }
}