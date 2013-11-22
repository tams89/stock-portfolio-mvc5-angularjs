// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PortfolioMap.cs" company="">
//   
// </copyright>
// <summary>
//   The portfolio map.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Core.Models.HFT;
using DapperExtensions.Mapper;

namespace Core.ORM.Mappings.Portfolio
{
    /// <summary>
    /// The portfolio map.
    /// </summary>
    public class PortfolioMap : ClassMapper<Models.Portfolio.Portfolio>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PortfolioMap"/> class.
        /// </summary>
        public PortfolioMap()
        {
            Schema("Portfolio");
            Table("Portfolio");
            Map(f => f.PortfolioId).Key(KeyType.Guid);
            Map(f => f.UserId).Column("UserId");
        }
    }
}