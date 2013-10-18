using Core.Models.Portfolio;
using DapperExtensions.Mapper;

namespace Core.ORM.Mappings.Portfolio
{
    public class PortfolioSecuritiesMap : ClassMapper<PortfolioSecurities>
    {
        public PortfolioSecuritiesMap()
        {
            base.Schema("Portfolio");
            base.Table("Portfolio_Securities");
            Map(f => f.Id).Key(KeyType.Guid);
        }
    }
}