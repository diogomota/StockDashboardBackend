using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace StockDashboardBackend.Common
{
    /// <summary>
    /// Supplies data needed by the httpClient to build the request
    /// And builds the request url
    /// </summary>
    public class RequestPlan
    {
        public string EndPoint { get; }
        public IReadOnlyDictionary<string,string> RequestArguments { get; }

        public string RequestUrl { get; private set; }

        public RequestPlan(string endpoint, IReadOnlyDictionary<string, string> arguments)
        {
            EndPoint = endpoint.TrimEnd('/');
            RequestArguments = arguments ?? new Dictionary<string, string>();
            Build();
        }

        private void Build()
        {
            string request = EndPoint+"?";

            foreach (KeyValuePair<string, string> kvp in RequestArguments)
            {
                request += $"{kvp.Key}={kvp.Value}&";
            }

            RequestUrl= request.TrimEnd("&?".ToCharArray());
        }
    }
}