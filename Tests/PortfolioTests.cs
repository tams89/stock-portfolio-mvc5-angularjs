using System.Linq;
using Core.Models.Portfolio;
using Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class PortfolioTests
    {
        [TestMethod]
        public void GetSymbols()
        {
            var service = new PortfolioService<Security>().SecurityList();
            Assert.IsTrue(service.Any());
        }
    }
}