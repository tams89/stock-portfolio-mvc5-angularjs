using Core.Models.HFT;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HFTTests
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
            var tick = Tick.GetAll();
            Assert.IsNotNull(tick);
        }
    }
}