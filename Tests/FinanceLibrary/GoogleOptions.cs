using System;
using System.Linq;
using AlgoTrader.GoogleApi;
using Microsoft.FSharp.Core;
using NUnit.Framework;

namespace Test.FinanceLibrary
{
    [TestFixture]
    public class GoogleOptions
    {
        [Test]
        public void CanGetOptionChain_FromGoogle()
        {
            var optionChain = Options.GetOptionsData("GOOG", new FSharpOption<DateTime>(DateTime.Now));
            Assert.IsTrue(optionChain.Calls.Any());
        }
    }
}