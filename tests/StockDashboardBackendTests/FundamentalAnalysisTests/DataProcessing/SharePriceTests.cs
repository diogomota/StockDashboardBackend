using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using StockDashboardBackend.FundamentalAnalysis.DataProcessing;

namespace StockDashboardBackendTests.FundamentalAnalysisTests.DataProcessing
{
    public class SharePriceTests
    {
        [Test]
        public async Task SharePrice_ValidTicker_returnsNonNullValue()
        {
            string ticker = "MCD";

            string result = await SharePrice.Get(ticker, new HttpClient());
            
            Assert.NotNull(result);
        }
        
        [Test]
        public async Task SharePrice_InvalidTicker_NullValue()
        {
            string ticker = "PLASDPA";

            string result = await SharePrice.Get(ticker, new HttpClient());
            
            Assert.IsNull(result);
        }
    }
}