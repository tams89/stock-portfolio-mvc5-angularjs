// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PortfolioTests.cs" company="">
//   
// </copyright>
// <summary>
//   The portfolio tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System.Linq;
using Core.Models.Portfolio;
using Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    /// <summary>
    /// The portfolio tests.
    /// </summary>
    [TestClass]
    public class PortfolioTests
    {
        /// <summary>
        /// The get symbols.
        /// </summary>
        [TestMethod]
        public void GetSymbols()
        {
            var service = new PortfolioService<Security>().SecurityList();
            Assert.IsTrue(service.Any());
        }
    }
}