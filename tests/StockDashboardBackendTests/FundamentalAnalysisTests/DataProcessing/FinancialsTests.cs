using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using StockDashboardBackend.FundamentalAnalysis.DataProcessing;

namespace StockDashboardBackendTests.FundamentalAnalysisTests.DataProcessing
{
    public class FinancialsTests
    {
        [Test]
        public async Task Financials_ValidArguments_ReturnsDictionary()
        {
            var output = await Financials.Get("MCD", new HttpClient());
            
            Assert.NotNull(output);
        }
        [Test]
        public async Task Financials_invalidArguments_ReturnsDictionary()
        {
            var output = await Financials.Get("MCDadsd", new HttpClient());
            
            Assert.IsNull(output);
        }
        
    }
}