using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockDashboardBackend.Common.Services
{
    public abstract class RequestHandler
    {
        private IHttpClientFactory _clientFactory;

        private IReadOnlyList<string> _outputs;

        public RequestHandler(IHttpClientFactory clientFactory, IReadOnlyList<string> outputNames)
        {
            _clientFactory = clientFactory;
            _outputs = outputNames;
        }

        /*private async Task<string> Get()
        {
            
        }*/
        
        //public async Task<Dictionary<>
    }
}