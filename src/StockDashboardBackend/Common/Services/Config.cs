using System.IO;
using System.Net;
using System.Text.Json;

namespace StockDashboardBackend.Common.Services
{
    /// <summary>
    /// These will be stored in a key vault when moved to aws.. for now its just json
    /// </summary>
    public class Config
    {
        public string BaseEndpoint { get; set; }
        public string Key { get; set; }
        
        public Config()
        {
            //Prod credentials are fetched from aws and will not leave that location
            string path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(),
                "config.json");
            var contents = File.ReadAllText(path);
            using (JsonDocument doc = JsonDocument.Parse(contents))
            {
                this.BaseEndpoint = doc.RootElement.GetProperty("BaseEndpoint").ToString();
                this.Key = doc.RootElement.GetProperty("Key").ToString();
            }
        }
    }
}