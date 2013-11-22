// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YahooFinanceServiceTests.cs" company="">
//   
// </copyright>
// <summary>
//   The yahoo finance service tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Services;
using Core.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    /// <summary>
    /// The yahoo finance service tests.
    /// </summary>
    [TestClass]
    public class YahooFinanceServiceTests
    {
        /// <summary>
        /// The _yahoo finance service.
        /// </summary>
        private readonly IYahooFinanceService _yahooFinanceService = new YahooFinanceService();

        /// <summary>
        /// The get stock data.
        /// </summary>
        [TestMethod]
        public void GetStockData()
        {
            var marketData = _yahooFinanceService.GetData("MSFT", null, null);
            Assert.IsNotNull(marketData);
        }

        /// <summary>
        /// The get option data.
        /// </summary>
        [TestMethod]
        public void GetOptionData()
        {
            var optionData = _yahooFinanceService.GetData("GOOG");
            Assert.IsNotNull(optionData);
        }
    }
}