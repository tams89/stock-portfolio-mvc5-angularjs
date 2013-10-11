using DapperExtensions.Mapper;
using Domain.Models.HFT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Mappings.HFT
{
    public class TickMapper : ClassMapper<Tick>
    {
        public TickMapper()
        {
            base.Schema("HFT");
        }
    }
}