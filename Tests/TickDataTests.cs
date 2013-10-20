using System.Linq;
using Core.Models.HFT;
using Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class TickDataTests
    {
        /// <summary>
        /// Test to see if all tick data can be retrieved from db.
        /// </summary>
        [TestMethod]
        public void GetTickTest()
        {
            var tick = new HFTService<Tick>().Get();
            Assert.IsNotNull(tick.Count() > 10);
        }
    }
}