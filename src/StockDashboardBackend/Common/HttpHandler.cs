using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace StockDashboardBackend.FundamentalAnalysis
{
    public class HttpHandler
    {
        private readonly HttpClient _client;
        
        private readonly string _requestUrl;
        
        public HttpHandler(HttpClient httpClient, string url)
        {
            _client = httpClient;
            _requestUrl = url;
        }

        public async Task<string> Get()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,_requestUrl);
            
            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return null;
                //throw new HttpRequestException($"Failed with Status code: {response.StatusCode.ToString()}");
            }
        }
        
    }
}