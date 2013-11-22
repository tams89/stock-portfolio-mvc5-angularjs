// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tick.cs" company="">
//   
// </copyright>
// <summary>
//   Tick Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------



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
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the symbol.
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Gets or sets the open.
        /// </summary>
        public decimal Open { get; set; }

        /// <summary>
        /// Gets or sets the high.
        /// </summary>
        public decimal High { get; set; }

        /// <summary>
        /// Gets or sets the low.
        /// </summary>
        public decimal Low { get; set; }

        /// <summary>
        /// Gets or sets the close.
        /// </summary>
        public decimal Close { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        public int Volume { get; set; }
    }
}