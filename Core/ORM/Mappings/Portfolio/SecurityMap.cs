using Core.Models.Portfolio;
using DapperExtensions.Mapper;

namespace Core.ORM.Mappings.Portfolio
{
    public class SecurityMap : ClassMapper<Security>
    {
        public SecurityMap()
        {
            base.Schema("Portfolio");
            base.Table("Security");
            Map(f => f.SecurityId).Key(KeyType.Guid);
        }
    }
}