using AlgoTrader;
using AlgoTrader.YahooApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Test
{
    [TestClass]
    public class FinanceLibararyTests
    {
        [TestMethod]
        public void GetOptionsData()
        {
            var options = Options.GetOptionsData("MSFT");
            Assert.IsTrue(options.Count() > 1);
        }

        [TestMethod]
        public void BlackScholesTest()
        {
            var googleOption = Options.GetOptionsData("GOOG").First();

            BlackScholes.Type optionType = null;
            if (googleOption.Type == "C") optionType = BlackScholes.Type.Call;
            if (googleOption.Type == "P") optionType = BlackScholes.Type.Put;

            //var blackScholesValue = BlackScholes.Black_Scholes(optionType, googleOption., 0, 0, 0, 0);
        }
    }
}