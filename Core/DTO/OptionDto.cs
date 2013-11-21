// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OptionDto.cs" company="">
//   
// </copyright>
// <summary>
//   The option dto.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Core.DTO
{
    using System;

    /// <summary>
    /// The option dto.
    /// </summary>
    public class OptionDto : DtoBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the ask.
        /// </summary>
        public decimal Ask { get; set; }

        /// <summary>
        /// Gets or sets the bid.
        /// </summary>
        public decimal Bid { get; set; }

        /// <summary>
        /// Gets or sets the change.
        /// </summary>
        public decimal Change { get; set; }

        /// <summary>
        /// Gets or sets the change direction.
        /// </summary>
        public string ChangeDirection { get; set; }

        /// <summary>
        /// Gets or sets the close.
        /// </summary>
        public decimal Close { get; set; }

        /// <summary>
        /// Gets or sets the expiry date.
        /// </summary>
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets the last price.
        /// </summary>
        public decimal LastPrice { get; set; }

        /// <summary>
        /// Gets or sets the open int.
        /// </summary>
        public int OpenInt { get; set; }

        /// <summary>
        /// Gets or sets the strike price.
        /// </summary>
        public decimal StrikePrice { get; set; }

        /// <summary>
        /// Gets or sets the symbol.
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the vol.
        /// </summary>
        public int Vol { get; set; }

        #endregion
    }
}