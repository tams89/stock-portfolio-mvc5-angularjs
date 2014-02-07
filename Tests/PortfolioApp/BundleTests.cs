using NUnit.Framework;
using System.Web.Optimization;

namespace Test.PortfolioApp
{
    [TestFixture]
    public class BundleTests
    {
        [Test]
        public void DoBundlesRegister()
        {
            var bundleCollection = new BundleCollection();
            Portfolio.App_Start.BundleConfig.RegisterBundles(bundleCollection);

            Assert.IsTrue(bundleCollection.Count > 1);
        }
    }
}
