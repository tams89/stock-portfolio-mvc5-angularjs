using AlgoTrader.Core.Models.HFT;
using DapperExtensions.Mapper;

namespace AlgoTrader.Core.Dapper.Mapping.HFT
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

            Map(x => x.Symbol).Column("Symbol");
            Map(x => x.Date).Column("Date");
            Map(x => x.Time).Column("Time");
            Map(x => x.Open).Column("Open");
            Map(x => x.High).Column("High");
            Map(x => x.Low).Column("Low");
            Map(x => x.Close).Column("Close");
            Map(x => x.Volume).Column("Volume");
        }

        #endregion
    }
}