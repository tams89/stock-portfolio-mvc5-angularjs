// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Portfolio.cs" company="">
//   
// </copyright>
// <summary>
//   The portfolio.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Portfolio
{
    /// <summary>
    /// The portfolio.
    /// </summary>
    public class Portfolio
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Portfolio"/> class.
        /// </summary>
        public Portfolio()
        {

        }

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
