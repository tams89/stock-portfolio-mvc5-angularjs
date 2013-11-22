// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Security.cs" company="">
//   
// </copyright>
// <summary>
//   The security.
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
    /// The security.
    /// </summary>
    public class Security
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Security"/> class.
        /// </summary>
        public Security()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Security"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="symbol">
        /// The symbol.
        /// </param>
        public Security(Guid id, string symbol)
        {
            SecurityId = id;
            Symbol = symbol;
        }

        /// <summary>
        /// Gets or sets the security id.
        /// </summary>
        public Guid SecurityId { get; set; }

        /// <summary>
        /// Gets or sets the symbol.
        /// </summary>
        public string Symbol { get; set; }
    }
}
