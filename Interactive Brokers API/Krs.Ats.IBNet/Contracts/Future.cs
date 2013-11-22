// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Future.cs" company="">
//   
// </copyright>
// <summary>
//   Future Class - uses default constructors for creating an future contract.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;

namespace Krs.Ats.IBNet.Contracts
{
    /// <summary>
    /// Future Class - uses default constructors for creating an future contract.
    /// </summary>
    /// <seealso cref="Contract"/>
    [Serializable()]
    public class Future : Contract
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Future"/> class. 
        /// Create n Future Contract for a specific exchange
        /// </summary>
        /// <param name="symbol">
        /// Symbol for the future contract. See <see cref="Contract.Symbol"/>.
        /// </param>
        /// <param name="exchange">
        /// Exchange for the future contract. See <see cref="Contract.Exchange"/>.
        /// </param>
        /// <param name="expiry">
        /// Expiry for a future contract. See <see cref="Contract.Expiry"/>.
        /// </param>
        public Future(string symbol, string exchange, string expiry)
            : base(symbol, exchange, SecurityType.Future, "USD", expiry)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Future"/> class. 
        /// Create a Future Contract for a specific exchange
        /// </summary>
        /// <param name="symbol">
        /// Symbol for the future contract. See <see cref="Contract.Symbol"/>.
        /// </param>
        /// <param name="exchange">
        /// Exchange for the future contract. See <see cref="Contract.Exchange"/>.
        /// </param>
        /// <param name="expiry">
        /// Expiry for a future contract. See <see cref="Contract.Expiry"/>.
        /// </param>
        /// <param name="currency">
        /// Currency for a future contract. See <see cref="Contract.Currency"/>.
        /// </param>
        public Future(string symbol, string exchange, string expiry, string currency)
            : base(symbol, exchange, SecurityType.Future, currency, expiry)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Future"/> class. 
        /// Create a Future Contract for a specific exchange
        /// </summary>
        /// <param name="symbol">
        /// Symbol for the future contract. See <see cref="Contract.Symbol"/>.
        /// </param>
        /// <param name="exchange">
        /// Exchange for the future contract. See <see cref="Contract.Exchange"/>.
        /// </param>
        /// <param name="expiry">
        /// Expiry for a future contract. See <see cref="Contract.Expiry"/>.
        /// </param>
        /// <param name="currency">
        /// Currency for a future contract. See <see cref="Contract.Currency"/>.
        /// </param>
        /// <param name="multiplier">
        /// Multiplier for a future contract. See <see cref="Contract.Multiplier"/>.
        /// </param>
        public Future(string symbol, string exchange, string expiry, string currency, decimal multiplier)
            : base(symbol, exchange, SecurityType.Future, currency, expiry)
        {
            Multiplier = multiplier.ToString();
        }
    }
}
