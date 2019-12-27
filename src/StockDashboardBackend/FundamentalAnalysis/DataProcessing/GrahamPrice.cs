using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using StockDashboardBackend.Common.Services;

namespace StockDashboardBackend.FundamentalAnalysis.DataProcessing
{
    public static class GrahamPrice
    {
        private static Config _config = new Config();
        //Updated December 2019 from Moody's
        private static readonly double USA_AAA_20Yr_CorporateBondYield = 3.06;
        
        /// <summary>
        /// Returns graham's stock valuation
        /// Currently if no assumed growth rate is provided a 3% growth is assumed
        /// This will be updated once a better method of infering growth estimates is implemented
        /// based on other stock metrics
        /// </summary>
        /// <param name="ticker"></param>
        /// <param name="client"></param>
        /// <param name="growthRateOverride">(in %) i.e: 3 = 3%</param>
        /// <returns></returns>
        public static async Task<string> Get(string ticker, HttpClient client,double? growthRateOverride=3)
        {
            string request = BuildUrl(ticker);
            
            HttpHandler handler = new HttpHandler(client,request);

            string rawData = await handler.Get();

            return ProcessData(rawData, growthRateOverride) ?? null;
        }

        private static string BuildUrl(string ticker)
        {
            return _config.BaseEndpoint.TrimEnd('/') +
                   $"/stable/stock/{ticker}/earnings?" +
                   $"token={_config.Key}";
        }

        private static string BuildIncStatementUrl(string ticker)
        {
            return "TODO";
        }

        private static string ProcessData(string rawData,double? growthRate = null)
        {
            if (rawData == null)
            {
                return null;
            }

            using (JsonDocument doc = JsonDocument.Parse(rawData))
            {
                var earnings = doc.RootElement.GetProperty("earnings")[0];
                
                if (growthRate != null)
                {
                    if (double.TryParse(earnings.GetProperty("actualEPS").ToString(), out var eps))
                    {
                        return $"{GrahamCalc(eps, growthRate)}";
                    }
                }
                else
                {
                    //Todo: Find a good way to guestimate a growth rate based on other indicators
                    //See past python version of this script for ideas
                    /*double.TryParse(earnings.GetProperty("EPSSurpriseDollar").ToString(), out var epsSurprise);
                    double.TryParse(earnings.GetProperty("consensusEPS").ToString(), out var consensusEps);

                    double suprisePercent = epsSurprise / consensusEps;*/



                    return null;
                }
            }

            return null;

        }

        private static double GrahamCalc(double eps, double? growthRate)
        {
            return (eps * (8.5 + 2.0*(growthRate??USA_AAA_20Yr_CorporateBondYield)) * 4.4) / USA_AAA_20Yr_CorporateBondYield;
        }
    }
}