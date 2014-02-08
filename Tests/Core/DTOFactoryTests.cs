using Core.DTO;
using Core.Factory;
using NUnit.Framework;

namespace Test.Core
{
    [TestFixture]
    public class DTOFactoryTests
    {
        [Test]
        public static void Create_Dtos()
        {
            var optionDto = DtoFactory<OptionDto>.Create();
            var marketDto = DtoFactory<MarketDto>.Create();
            var optionDtos = DtoFactory<OptionDto>.CreateList();
            var marketDtos = DtoFactory<MarketDto>.CreateList();

            Assert.IsTrue(optionDto != null && marketDto != null && optionDtos != null && marketDtos != null);
        }
    }
}
