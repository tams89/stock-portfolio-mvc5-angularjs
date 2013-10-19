using System;
using System.Data.SqlClient;
using Core.Models.Portfolio;
using Core.Services;
using DapperExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class DbSetup
    {
        [TestInitialize]
        public void SetupDb()
        {
            //var user = new User(Guid.NewGuid(), WindowsIdentity.GetCurrent().Name);
            //var x1 = Insert(user);

            //var portfolio = new Portfolio(Guid.NewGuid(), x1);
            //var x2 = Insert(portfolio);

            //var security = new Security(Guid.NewGuid(), "GOOG");
            //var x3 = Insert(security);

            //var portfolioSecurity = new PortfolioSecurity(Guid.NewGuid(), x2, x3);
            //var x4 = Insert(portfolioSecurity);
        }

        public Guid Insert<T>(T item) where T : class
        {
            using (var c = new SqlConnection(Core.ORM.Constants.AlgoTradingDbConnectionStr))
            {
                c.Open();
                var x = c.Insert<T>(item);
                c.Close();
                return x;
            }
        }

        [TestMethod]
        public void CheckUserData()
        {
            var x = new PortfolioService<User>().GetAll();
            Assert.IsNotNull(x);
        }

        [TestMethod]
        public void CheckPortfolio()
        {
            var x = new PortfolioService<Portfolio>().GetAll();
            Assert.IsNotNull(x);
        }
        [TestMethod]
        public void CheckSecurityData()
        {
            var x = new PortfolioService<Security>().GetAll();
            Assert.IsNotNull(x);
        }

        [TestMethod]
        public void CheckPortfolioSecurityData()
        {
            var x = new PortfolioService<PortfolioSecurity>().GetAll();
            Assert.IsNotNull(x);
        }
    }
}