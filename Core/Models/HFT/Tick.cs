using System;
using AlgoTrader.Core.Repository;

namespace AlgoTrader.Core.Models.HFT
{
    /// <summary>
    /// Tick Model
    /// </summary>
    public class Tick : IEntity
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