using System.Net.Http;
using System.Threading.Tasks;
using StockDashboardBackend.Common.Services;

namespace StockDashboardBackend.FundamentalAnalysis.DataProcessing
{
    public static class SharePrice
    {
        private static Config _config = new Config();
        
        public static async Task<string> Get(string ticker,HttpClient client)
        {
            string request = BuildUrl(ticker);
            
            HttpHandler handler = new HttpHandler(client, request);
            
            string rawData = await handler.Get();

            return ProcessData(rawData);
        }

        private static string BuildUrl(string ticker)
        {
            return _config.BaseEndpoint.TrimEnd('/') + $"/v1/stock/{ticker}/price" + $"?token={_config.Key}";
        }
        private static string ProcessData(string rawData)
        {
            if (rawData == null)
            {
                return null;
            }
            return rawData;
        }
        

    }
}