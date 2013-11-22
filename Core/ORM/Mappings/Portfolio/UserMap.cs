// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserMap.cs" company="">
//   
// </copyright>
// <summary>
//   The user map.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Core.Models.Portfolio;
using DapperExtensions.Mapper;

namespace Core.ORM.Mappings.Portfolio
{
    /// <summary>
    /// The user map.
    /// </summary>
    public class UserMap : ClassMapper<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserMap"/> class.
        /// </summary>
        public UserMap()
        {
            Schema("Portfolio");
            Table("User");
            Map(f => f.UserId).Key(KeyType.Guid);
            Map(f => f.UserName).Column("UserName");
        }
    }
}