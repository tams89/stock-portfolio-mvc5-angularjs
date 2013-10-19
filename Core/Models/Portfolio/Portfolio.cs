using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Portfolio
{
    public class Portfolio
    {
        public Portfolio()
        {

        }

        public Portfolio(Guid id, Guid userId)
        {
            PortfolioId = id;
            UserId = userId;
        }

        public Guid PortfolioId { get; set; }
        public Guid UserId { get; set; }
    }
}
