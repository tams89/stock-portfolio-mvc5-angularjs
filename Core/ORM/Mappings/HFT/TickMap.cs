using Core.Models.HFT;
using DapperExtensions.Mapper;

namespace Core.ORM.Mappings.HFT
{
    public class TickMap : ClassMapper<Tick>
    {
        public TickMap()
        {
            base.Schema("HFT");
            base.Table("Tick");
            Map(f => f.Id).Key(KeyType.Guid);
        }
    }
}