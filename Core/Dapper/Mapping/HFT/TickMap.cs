using Core.Models.HFT;
using DapperExtensions.Mapper;

namespace Core.Dapper.Mapping.HFT
{
    /// <summary>
    /// The tick map.
    /// </summary>
    public sealed class TickMap : ClassMapper<Tick>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TickMap" /> class.
        /// </summary>
        public TickMap()
        {
            Schema("HFT");
            Table("Tick");
            Map(f => f.Id).Key(KeyType.Guid);
        }

        #endregion
    }
}