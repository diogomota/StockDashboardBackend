using System;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using StockDashboardBackend.Common.Services;

namespace StockDashboardBackend.FundamentalAnalysis.DataProcessing
{
    public class PeriodGrowth
    {
        private static Config _config = new Config();
        
        /// <summary>
        /// Returns growth in the month range provided
        /// </summary>
        /// <param name="ticker"></param>
        /// <param name="client"></param>
        /// <param name="MonthRange"></param>
        /// <returns></returns>
        public static async Task<string> Get(string ticker, HttpClient client, int monthRange = 6)
        {
            string request = BuildUrl(ticker, monthRange);
            
            HttpHandler handler = new HttpHandler(client,request);

            string rawData = await handler.Get();

            string currentPrice = await SharePrice.Get(ticker, client);

            if (rawData != null && currentPrice != null)
            {
                return ProcessData(rawData, currentPrice);
            }
            else
            {
                return "Error on PeriodGrowth()";
                //Exception?
            }
        }
        
        private static string BuildUrl(string ticker,int months)
        {
            return _config.BaseEndpoint.TrimEnd('/') + 
                   $"/stable/stock/{ticker}/chart/date/{PastWorkingDayDate(months)}?chartByDay=true&" +
                   $"token={_config.Key}";
        }

        private static string PastWorkingDayDate(int months)
        {
            var past = DateTime.Now.Subtract(TimeSpan.FromDays(30*months));

            if (past.DayOfWeek == DayOfWeek.Saturday)
            {
                past = past.Subtract(TimeSpan.FromDays(1));
            }

            if (past.DayOfWeek == DayOfWeek.Sunday)
            {
                past = past.Subtract(TimeSpan.FromDays(2));
            }

            return $"{past.Year}{past.Month.ToString().PadLeft(2,'0')}{past.Day.ToString().PadLeft(2,'0')}";
        }
        private static string ProcessData(string rawData,string presentPrice)
        {
            if (rawData == null || presentPrice == null)
            {
                return null;
            }
            try
            {
                if (!double.TryParse(presentPrice, out var present))
                {
                    throw new InvalidCastException("Failed to parse current price to double in PeriodGrowth()");
                }

                using (JsonDocument doc = JsonDocument.Parse(rawData))
                {
                    if (double.TryParse(doc.RootElement[0].GetProperty("close").ToString(), out var pastV))
                    {
                        return $"{(100 * ((present / pastV) - 1))}";
                    }

                    throw new InvalidCastException("Failed to parse past price to double in PeriodGrowth()");
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}