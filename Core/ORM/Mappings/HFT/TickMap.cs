// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TickMap.cs" company="">
//   
// </copyright>
// <summary>
//   The tick map.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using Core.Models.HFT;
using DapperExtensions.Mapper;

namespace Core.ORM.Mappings.HFT
{
    /// <summary>
    /// The tick map.
    /// </summary>
    public class TickMap : ClassMapper<Tick>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TickMap"/> class.
        /// </summary>
        public TickMap()
        {
            this.Schema("HFT");
            this.Table("Tick");
            Map(f => f.Id).Key(KeyType.Guid);
        }
    }
}