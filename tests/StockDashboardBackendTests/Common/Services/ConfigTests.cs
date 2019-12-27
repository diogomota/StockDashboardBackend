using NUnit.Framework;
using StockDashboardBackend.Common.Services;

namespace StockDashboardBackendTests.Common.Services
{
    public class ConfigTests
    {
        [Test]
        public void Config_LoadsCorrectSettings()
        {
            var obj = new Config();
            
            Assert.NotNull(obj.BaseEndpoint);
            Assert.NotNull(obj.Key);
        }
    }
}