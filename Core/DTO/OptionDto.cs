

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

        /// <summary>
        /// Is the option in the money? 
        /// </summary>
        public bool InTheMoney { get; set; }

        /// <summary>
        /// Is the option at the money?
        /// </summary>
        public bool AtTheMoney { get; set; }

        /// <summary>
        /// The number of days (fractional) until the option expires.
        /// </summary>
        public string DaysToExpiry { get; set; }

        /// <summary>
        /// Gets or sets the volatility.
        /// </summary>
        public double Volatility { get; set; }

        /// <summary>
        /// The Black Scholes price for the option.
        /// </summary>
        public double BlackScholes { get; set; }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Strike: {0} Last: {1} BlackScholes: {2}", StrikePrice, LastPrice, BlackScholes);
        }

        #endregion
    }
}