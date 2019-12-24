using System.Dynamic;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Text.Json;

namespace StockDashboardBackend.FundamentalAnalysis
{
    public class HttpHandlerFundamentalAnalysis
    {
        private IHttpClientFactory _clientFactory;
        private string RequestUrl;
        
        public HttpHandlerFundamentalAnalysis(IHttpClientFactory clientFactory, string url)
        {
            _clientFactory = clientFactory;
            RequestUrl = url;
        }

        public async Task<FundamentalAnalysisResults> Get()
        {
            //TODO: look into making this config before calling this class.. ie client comes already configured
            //from caller class
            
            var request = new HttpRequestMessage(HttpMethod.Get,RequestUrl);

            var client = _clientFactory.CreateClient("FundamentalAnalysis");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<FundamentalAnalysisResults>(
                    await response.Content.ReadAsStreamAsync());
            }
            else
            {
                //Failure should return empty
                return null;
            }
        }
        
    }
}