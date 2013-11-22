// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecurityMap.cs" company="">
//   
// </copyright>
// <summary>
//   The security map.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Core.Models.Portfolio;
using DapperExtensions.Mapper;

namespace Core.ORM.Mappings.Portfolio
{
    /// <summary>
    /// The security map.
    /// </summary>
    public class SecurityMap : ClassMapper<Security>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityMap"/> class.
        /// </summary>
        public SecurityMap()
        {
            Schema("Portfolio");
            Table("Security");
            Map(f => f.SecurityId).Key(KeyType.Guid);
            Map(f => f.Symbol).Column("Symbol");
        }
    }
}