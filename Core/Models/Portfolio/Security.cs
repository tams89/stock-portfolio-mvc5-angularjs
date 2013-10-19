using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Portfolio
{
    public class Security
    {
        public Security()
        {
            
        }

        public Security(Guid id, string symbol)
        {
            SecurityId = id;
            Symbol = symbol;
        }

        public Guid SecurityId { get; set; }
        public string Symbol { get; set; }
    }
}
