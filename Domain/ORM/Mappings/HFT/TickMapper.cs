using Core.Models.HFT;
using DapperExtensions.Mapper;

namespace Core.ORM.Mappings.HFT
{
    public class TickMapper : ClassMapper<Tick>
    {
        public TickMapper()
        {
            base.Schema("HFT");
            base.Table("Tick");
            Map(f => f.Id).Key(KeyType.Guid);
        }
    }
}