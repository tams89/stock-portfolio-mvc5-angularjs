using Core.Models.Portfolio;
using DapperExtensions.Mapper;

namespace Core.ORM.Mappings.Portfolio
{
    public class PortfolioSecuritiesMap : ClassMapper<PortfolioSecurity>
    {
        public PortfolioSecuritiesMap()
        {
            base.Schema("Portfolio");
            base.Table("Portfolio_Security");
            Map(f => f.Id).Key(KeyType.Guid);
            Map(f => f.PortfolioId).Column("PortfolioId");
            Map(f => f.SecurityId).Column("SecurityId");
        }
    }
}