using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Portfolio
{
    public class PortfolioSecurities
    {
        public Guid Id { get; set; }
        public Portfolio Portfolio { get; set; }
        public Security Security { get; set; }
    }
}
