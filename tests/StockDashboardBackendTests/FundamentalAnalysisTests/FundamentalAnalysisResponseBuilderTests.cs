using System.Threading.Tasks;
using NUnit.Framework;
using StockDashboardBackend.FundamentalAnalysis;

namespace StockDashboardBackendTests.FundamentalAnalysisTests
{
    public class FundamentalAnalysisResponseBuilderTests
    {
        [Test]
        public async Task FundamentalAnaysisResponseBuilder_ValidArguments_ReturnsDto()
        {
            var result = await FundamentalAnalysisResponseBuilder.Build("MCD", 3, 3.0);
            
            Assert.NotNull(result);
        }
    }
}