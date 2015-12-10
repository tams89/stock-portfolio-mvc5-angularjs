using System;

namespace AlgoTrader.Core.Models.Portfolio
{
    /// <summary>
    /// The portfolio.
    /// </summary>
    public class Portfolio
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Portfolio"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        public Portfolio(Guid id, Guid userId)
        {
            PortfolioId = id;
            UserId = userId;
        }

        /// <summary>
        /// Gets or sets the portfolio id.
        /// </summary>
        public Guid PortfolioId { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public Guid UserId { get; set; }
    }
}
