using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using StockDashboardBackend.FundamentalAnalysis.DataProcessing;

namespace StockDashboardBackend.FundamentalAnalysis
{
    public static class FundamentalAnalysisResponseBuilder
    {
        public static async Task<string> Build(string ticker, int growthInterval = 3,double GrahamGrowthRateOverride =3,string endpoint=null,string apiKey=null)
        {
            var defaultClient = new HttpClient();
            string sharePrice = await SharePrice.Get(ticker, defaultClient,endpoint,apiKey) ?? "N/a";
            string periodGrowth = await PeriodGrowth.Get(ticker, defaultClient, growthInterval,endpoint,apiKey)??"N/a";

            Dictionary<string, string> financials = await Financials.Get(ticker, defaultClient,endpoint,apiKey);
            
            string roe = financials["roe"]?? "N/a";
            string rota = financials["rota"]?? "N/a";
            string currentRatio = financials["currentRatio"]?? "N/a";
            string debtToEquity = financials["debtToEquity"] ?? "N/a";

            string graham = await GrahamPrice.Get(ticker, defaultClient, GrahamGrowthRateOverride,endpoint,apiKey)??"N/a";
            
            FundamentalAnalysisResults Dto = new FundamentalAnalysisResults(sharePrice,
                periodGrowth,rota,roe,debtToEquity,currentRatio,graham);

            return JsonSerializer.Serialize(Dto);
            
        }
    }
}