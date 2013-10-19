using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Portfolio
{
    public class PortfolioSecurity
    {
        public PortfolioSecurity()
        {
            
        }

        public PortfolioSecurity(Guid id, Guid portfolioId, Guid securityId)
        {
            Id = id;
            PortfolioId = portfolioId;
            SecurityId = securityId;
        }

        public Guid Id { get; set; }
        public Guid PortfolioId { get; set; }
        public Guid SecurityId { get; set; }
    }
}
