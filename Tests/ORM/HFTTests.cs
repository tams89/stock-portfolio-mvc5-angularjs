using Core;
using NUnit.Framework;

namespace Test.ORM
{
    /// <summary>
    /// The hft tests.
    /// </summary>
    [TestFixture]
    public class HFTTests
    {
        [Test]
        public void ConnectionStringPresent()
        {
            // Check that the connection string is available.
            Assert.IsTrue(Constants.AlgoTradingDbConnectionStr != null);
        }
    }
}