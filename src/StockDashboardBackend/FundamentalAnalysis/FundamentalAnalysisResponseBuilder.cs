using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using StockDashboardBackend.FundamentalAnalysis.DataProcessing;

namespace StockDashboardBackend.FundamentalAnalysis
{
    public static class FundamentalAnalysisResponseBuilder
    {
        public static async Task<object> Build(string ticker, int growthInterval = 3,double GrahamGrowthRateOverride =3)
        {
            var defaultClient = new HttpClient();
            string sharePrice = await SharePrice.Get(ticker, defaultClient) ?? "N/a";
            string periodGrowth = await PeriodGrowth.Get(ticker, defaultClient, growthInterval)??"N/a";

            Dictionary<string, string> financials = await Financials.Get(ticker, defaultClient);
            
            string roe = financials["roe"]?? "N/a";
            string rota = financials["rota"]?? "N/a";
            string currentRatio = financials["currentRatio"]?? "N/a";
            string debtToEquity = financials["debtToEquity"] ?? "N/a";

            string graham = await GrahamPrice.Get(ticker, defaultClient, GrahamGrowthRateOverride)??"N/a";
            
            FundamentalAnalysisResults Dto = new FundamentalAnalysisResults(sharePrice,
                periodGrowth,rota,roe,debtToEquity,currentRatio,graham);

            return JsonSerializer.Serialize(Dto);
            
        }
    }
}