using Core.EntityFramework;
using Core.Repository;
using Core.Repository.EntityFramework;
using NUnit.Framework;
using System.Linq;

namespace Test.Core
{
    [TestFixture]
    public class EntityFrameworkTests
    {
        private AlgorithmicTradingEntities entities;
        private IUnitOfWork unitOfWork;

        [SetUp]
        public void Init()
        {
            entities = new AlgorithmicTradingEntities();
            unitOfWork = new UnitOfWork();
        }

        [Test]
        public void CanEF_PerformSelect_FromAllPortfolioTables()
        {
            var users = entities.Users.ToList();
            var portfolio = entities.Portfolios.ToList();
            var securities = entities.Securities.ToList();
            var portfolioSecurities = entities.Portfolio_Security.ToList();

            Assert.IsNotEmpty(users);
            Assert.IsNotEmpty(portfolio);
            Assert.IsNotEmpty(securities);
            Assert.IsNotEmpty(portfolioSecurities);
        }

        [Test]
        public void UnitOfWork_GetPortfolios()
        {
            var data = unitOfWork.PortfolioRepository.GetAll().ToList();
            Assert.IsNotEmpty(data);
        }
    }
}
