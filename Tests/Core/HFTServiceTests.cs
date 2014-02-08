using Core;
using Core.Repository;
using Core.Repository.Dapper;
using Core.Services;
using Core.Services.Interfaces;
using NUnit.Framework;
using System.Linq;
using Tick = Core.Models.HFT.Tick;

namespace Test.Core
{
    [TestFixture]
    public class HFTServiceTests
    {
        private IReadOnlyRepository<Tick> tickRepository;
        private IHFTService hftService;

        [SetUp]
        public void Init()
        {
            AutoMapperConfig.Configure();
            tickRepository = new TickRepository();
            hftService = new HFTService(tickRepository);
        }

        [TestCase("IBM", true)]
        [TestCase("MSFT", false)]
        [TestCase("", false)]
        public void GetTickData_ByPredicate(string symbol, bool expected)
        {
            var data = hftService.BySymbol(symbol);
            Assert.IsTrue(data.Any() == expected);
        }

        [Test]
        public void GetAllTicks()
        {
            var data = hftService.AllTicks();
            Assert.IsNotEmpty(data);
        }

        [Test]
        public void TickById()
        {
            var tickId = hftService.BySymbol("IBM").First().Id;
            var byId = tickRepository.FindById(tickId);

            Assert.IsNotNull(byId);
        }
    }
}
