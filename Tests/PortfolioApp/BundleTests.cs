using NUnit.Framework;
using System.Web.Optimization;
using AlgoTrader.Portfolio;

namespace Test.PortfolioApp
{
    [TestFixture]
    public class BundleTests
    {
        [Test]
        public void DoBundlesRegister()
        {
            var bundleCollection = new BundleCollection();
            BundleConfig.RegisterBundles(bundleCollection);
            Assert.IsTrue(bundleCollection.Count > 1);
        }
    }
}
