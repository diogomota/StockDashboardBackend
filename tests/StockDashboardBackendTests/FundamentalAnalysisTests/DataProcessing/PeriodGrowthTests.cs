using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using StockDashboardBackend.FundamentalAnalysis.DataProcessing;

namespace StockDashboardBackendTests.FundamentalAnalysisTests.DataProcessing
{
    public class PeriodGrowthTests
    {
        [Test]
        public async Task PeriodGrowth_ValidArguments_ReturnsGrowth()
        {
            string growth = await PeriodGrowth.Get("MCD", new HttpClient(), 3);
            
            Assert.IsNotNull(growth);
        }
        [Test]
        public async Task PeriodGrowth_InvalidArguments_ReturnsNull()
        {
            string growth = await PeriodGrowth.Get("MCD", new HttpClient(), -3);
            
            Assert.IsNull(growth);
        }
        
    }
}