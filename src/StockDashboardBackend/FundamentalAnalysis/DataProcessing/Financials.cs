using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using StockDashboardBackend.Common.Services;

namespace StockDashboardBackend.FundamentalAnalysis.DataProcessing
{
    
    public static class Financials
    {
        private static Config _config= new Config();
        
        /// <summary>
        /// Returns:
        /// ROE
        /// ROTA
        /// Debt to Equity
        /// Current Ratio
        /// For the last year
        /// </summary>
        public static async Task<Dictionary<string, string>> Get(string ticker, HttpClient client)
        {
            string request = BuildUrl(ticker);
            
            HttpHandler handler = new HttpHandler(client,request);

            string rawData = await handler.Get();

            return ProcessData(rawData) ?? null;
        }

        private static string BuildUrl(string ticker)
        {
            return _config.BaseEndpoint.TrimEnd('/') +
                   $"/stable/stock/{ticker}/financials?period=annual" +
                   $"&token={_config.Key}";
        }

        private static Dictionary<string, string> ProcessData(string rawData)
        {
            if (rawData == null)
            {
                return null;
            }
            
            using (JsonDocument doc = JsonDocument.Parse(rawData))
            {
                try
                {
                    var _finStats = doc.RootElement.GetProperty("financials");
                    double.TryParse(_finStats[0].GetProperty("netIncome").ToString(), out var netIncome);
                    double.TryParse(_finStats[0].GetProperty("shareholderEquity").ToString(),
                        out var shareholderEq);
                    double.TryParse(_finStats[0].GetProperty("grossProfit").ToString(), out var grossProfit);
                    double.TryParse(_finStats[0].GetProperty("operatingExpense").ToString(),
                        out var operatingExpense);
                    double.TryParse(_finStats[0].GetProperty("currentDebt").ToString(), out var currentDebt);
                    double.TryParse(_finStats[0].GetProperty("currentAssets").ToString(), out var currentAssets);
                    double.TryParse(_finStats[0].GetProperty("totalLiabilities").ToString(), out var totalLiab);

                    return new Dictionary<string, string>()
                    {
                        {"roe", $"{netIncome / shareholderEq}"},
                        {"rota", $"{grossProfit / operatingExpense}"},
                        {"currentRatio", $"{currentDebt / currentAssets}"},
                        {"DebtToEquity", $"{totalLiab / shareholderEq}"}
                    };

                }
                catch (Exception e)
                {
                    return null;
                }

            }
        }
    }
}