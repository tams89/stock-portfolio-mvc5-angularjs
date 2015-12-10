using NUnit.Framework;
using System.Linq;
using AlgoTrader.Core.AutoMapper;
using AlgoTrader.Core.Models.HFT;
using AlgoTrader.Core.Repository;
using AlgoTrader.Core.Repository.Dapper;
using AlgoTrader.Core.Services;
using AlgoTrader.Core.Services.Interfaces;

namespace Test.Core
{
    [TestFixture]
    public class HftServiceTests
    {
        private IReadOnlyRepository<Tick> _tickRepository;
        private IHFTService _hftService;

        [SetUp]
        public void Init()
        {
            AutoMapperConfig.Configure();
            _tickRepository = new TickRepository();
            _hftService = new HFTService(_tickRepository);
        }

        [TestCase("IBM", true)]
        [TestCase("MSFT", false)]
        [TestCase("", false)]
        public void GetTickData_ByPredicate(string symbol, bool expected)
        {
            var data = _hftService.BySymbol(symbol);
            Assert.IsTrue(data.Any() == expected);
        }
    }
}
