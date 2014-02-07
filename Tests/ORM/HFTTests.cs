using Core.Models.HFT;
using Core.Services;
using NUnit.Framework;
using System.Linq;

namespace Test.ORM
{
    /// <summary>
    /// The hft tests.
    /// </summary>
    [TestFixture]
    public class HFTTests
    {
        /// <summary>
        /// Test to see if all tick data can be retrieved from db.
        /// </summary>
        [Test]
        public void GetTickTest()
        {
            var tick = new HFTService<Tick>().Get();
            Assert.IsNotNull(tick.Count() > 10);
        }
    }
}