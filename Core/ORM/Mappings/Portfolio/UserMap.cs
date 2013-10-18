using Core.Models.Portfolio;
using DapperExtensions.Mapper;

namespace Core.ORM.Mappings.Portfolio
{
    public class UserMap : ClassMapper<User>
    {
        public UserMap()
        {
            base.Schema("Portfolio");
            base.Table("User");
            Map(f => f.UserId).Key(KeyType.Guid);
        }
    }
}