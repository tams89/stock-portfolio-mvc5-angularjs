// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbSetupPortfolio.cs" company="">
//   
// </copyright>
// <summary>
//   The db setup portfolio.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Data.SqlClient;
using Core.Models.Portfolio;
using Core.ORM;
using Core.Services;
using DapperExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    /// <summary>
    /// The db setup portfolio.
    /// </summary>
    [TestClass]
    public class DbSetupPortfolio
    {
        /// <summary>
        /// The setup db.
        /// </summary>
        [TestInitialize]
        public void SetupDb()
        {
            // var user = new User(Guid.NewGuid(), WindowsIdentity.GetCurrent().Name);
            // var x1 = Insert(user);

            // var portfolio = new Portfolio(Guid.NewGuid(), x1);
            // var x2 = Insert(portfolio);

            // var security = new Security(Guid.NewGuid(), "GOOG");
            // var x3 = Insert(security);

            // var portfolioSecurity = new PortfolioSecurity(Guid.NewGuid(), x2, x3);
            // var x4 = Insert(portfolioSecurity);
        }

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Guid"/>.
        /// </returns>
        public Guid Insert<T>(T item) where T : class
        {
            using (var c = new SqlConnection(Constants.AlgoTradingDbConnectionStr))
            {
                c.Open();
                var x = c.Insert<T>(item);
                c.Close();
                return x;
            }
        }

        /// <summary>
        /// The check user data.
        /// </summary>
        [TestMethod]
        public void CheckUserData()
        {
            var x = new PortfolioService<User>().Get();
            Assert.IsNotNull(x);
        }

        /// <summary>
        /// The check portfolio.
        /// </summary>
        [TestMethod]
        public void CheckPortfolio()
        {
            var x = new PortfolioService<Portfolio>().Get();
            Assert.IsNotNull(x);
        }

        /// <summary>
        /// The check security data.
        /// </summary>
        [TestMethod]
        public void CheckSecurityData()
        {
            var x = new PortfolioService<Security>().Get();
            Assert.IsNotNull(x);
        }

        /// <summary>
        /// The check portfolio security data.
        /// </summary>
        [TestMethod]
        public void CheckPortfolioSecurityData()
        {
            var x = new PortfolioService<PortfolioSecurity>().Get();
            Assert.IsNotNull(x);
        }
    }
}