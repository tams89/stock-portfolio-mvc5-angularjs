// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PortfolioSecuritiesMap.cs" company="">
//   
// </copyright>
// <summary>
//   The portfolio securities map.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using Core.Models.Portfolio;
using DapperExtensions.Mapper;

namespace Core.ORM.Mappings.Portfolio
{
    /// <summary>
    /// The portfolio securities map.
    /// </summary>
    public class PortfolioSecuritiesMap : ClassMapper<PortfolioSecurity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PortfolioSecuritiesMap"/> class.
        /// </summary>
        public PortfolioSecuritiesMap()
        {
            this.Schema("Portfolio");
            this.Table("Portfolio_Security");
            Map(f => f.Id).Key(KeyType.Guid);
            Map(f => f.PortfolioId).Column("PortfolioId");
            Map(f => f.SecurityId).Column("SecurityId");
        }
    }
}