using Core;
using NUnit.Framework;

namespace Test.Core
{
    [TestFixture]
    public class AutoMapperTests
    {
        [Test]
        public void CheckMappingConfig()
        {
            Assert.DoesNotThrow(AutoMapperConfig.Configure);
        }
    }
}
