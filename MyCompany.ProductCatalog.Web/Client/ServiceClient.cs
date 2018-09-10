using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyCompany.ProductCatalog.Web.Client
{
    public class ServiceClient : IServiceClient
    {
        private HttpClient client;
        private string uri;

        public ServiceClient(IConfiguration configuration)
        {
            client = new HttpClient();
            uri = configuration.GetValue<string>("ProductServiceURI");
        }

        public async Task<T> ServiceRequestAsync<T>(HttpMethod httpMethod)
        {
            using (var httpMessage = new HttpRequestMessage(httpMethod, uri))
            {
                var response = await client.SendAsync(httpMessage);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(data);
                }
            }
            return default(T);
        }
    }
}
