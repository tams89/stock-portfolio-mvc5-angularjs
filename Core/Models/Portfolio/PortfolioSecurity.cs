// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PortfolioSecurity.cs" company="">
//   
// </copyright>
// <summary>
//   The portfolio security.
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
    /// The portfolio security.
    /// </summary>
    public class PortfolioSecurity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PortfolioSecurity"/> class.
        /// </summary>
        public PortfolioSecurity()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PortfolioSecurity"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="portfolioId">
        /// The portfolio id.
        /// </param>
        /// <param name="securityId">
        /// The security id.
        /// </param>
        public PortfolioSecurity(Guid id, Guid portfolioId, Guid securityId)
        {
            Id = id;
            PortfolioId = portfolioId;
            SecurityId = securityId;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the portfolio id.
        /// </summary>
        public Guid PortfolioId { get; set; }

        /// <summary>
        /// Gets or sets the security id.
        /// </summary>
        public Guid SecurityId { get; set; }
    }
}
