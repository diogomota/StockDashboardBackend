using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using StockDashboardBackend.FundamentalAnalysis.DataProcessing;

namespace StockDashboardBackendTests.FundamentalAnalysisTests.DataProcessing
{
    public class GrahamPriceTests
    {
        [Test]
        public async Task GrahamPrice_ValidParameters_ReturnsNonNullValue()
        {
            var result = await GrahamPrice.Get("MCD", new HttpClient(), 2);
            
            Assert.NotNull(result);
        }
        
        [Test]
        public async Task GrahamPrice_InvalidParameters_ReturnsNullValue()
        {
            var result = await GrahamPrice.Get("MCasD", new HttpClient(), 2);
            
            Assert.IsNull(result);
        }
    }
}