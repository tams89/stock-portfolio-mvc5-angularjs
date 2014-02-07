namespace Core.ORM.Mappings.HFT
{
    using DapperExtensions.Mapper;
    using Models.HFT;

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
            this.Schema("HFT");
            this.Table("Tick");
            this.Map(f => f.Id).Key(KeyType.Guid);
        }

        #endregion
    }
}