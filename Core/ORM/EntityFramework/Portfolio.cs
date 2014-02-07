namespace Core.ORM.EntityFramework
{
    using System.Collections.Generic;

    public partial class Portfolio
    {
        public Portfolio()
        {
            this.Portfolio_Security = new HashSet<Portfolio_Security>();
        }

        public System.Guid PortfolioId { get; set; }
        public System.Guid UserId { get; set; }

        public virtual ICollection<Portfolio_Security> Portfolio_Security { get; set; }
        public virtual User User { get; set; }
    }
}
