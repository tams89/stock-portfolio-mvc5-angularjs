using Core.Models.HFT;
using DapperExtensions.Mapper;

namespace Core.ORM.Mappings.Portfolio
{
    public class PortfolioMap : ClassMapper<Models.Portfolio.Portfolio>
    {
        public PortfolioMap()
        {
            base.Schema("Portfolio");
            base.Table("Portfolio");
            Map(f => f.PortfolioId).Key(KeyType.Guid);
            Map(f => f.UserId).Column("UserId");
        }
    }
}