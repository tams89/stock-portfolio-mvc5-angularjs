using System;
using System.Linq;
using AlgoTrader.GoogleApi;
using Microsoft.FSharp.Core;
using NUnit.Framework;

namespace Test.FinanceLibrary
{
    [TestFixture]
    public class GoogleOptionsTests
    {
        [Test]
        public void CanGetOptionChain_FromGoogle()
        {
            var optionChain = Options.GetOptionsData("GOOG", FSharpOption<DateTime>.None);
            Assert.IsTrue(optionChain.Calls.Any());
            Assert.IsTrue(optionChain.Puts.Any());
        }
    }
}