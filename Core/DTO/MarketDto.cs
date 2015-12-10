using System;

namespace AlgoTrader.Core.DTO
{
    /// <summary>
    /// The market dto.
    /// </summary>
    public class MarketDto : DtoBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the adj close.
        /// </summary>
        public double AdjClose { get; set; }

        /// <summary>
        /// Gets or sets the close.
        /// </summary>
        public double Close { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the high.
        /// </summary>
        public double High { get; set; }

        /// <summary>
        /// Gets or sets the low.
        /// </summary>
        public double Low { get; set; }

        /// <summary>
        /// Gets or sets the open.
        /// </summary>
        public double Open { get; set; }

        /// <summary>
        /// Gets or sets the symbol.
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        public int Volume { get; set; }

        #endregion
    }
}