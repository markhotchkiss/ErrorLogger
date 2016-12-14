using MJH.BusinessLogic.Configuration;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace MJH.UnitTests
{
    [TestFixture]
    public class UnitTestsConfiguration
    {
        [Test]
        public void ReadConfiguration()
        {
            var config = new ConfigurationHandler().Read();

            Assert.IsNotNull(config);
            Assert.IsNotNull(config.LoggingLevel);
        }

        [Test]
        public void WriteConfiguration()
        {
            var config = new ConfigurationHandler().Read();
            
            var writer = new ConfigurationHandler().Write(config);

            Assert.IsTrue(writer);
        }
    }
}
