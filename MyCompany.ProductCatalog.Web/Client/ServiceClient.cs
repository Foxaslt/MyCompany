using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
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

        public async Task<T> ServiceRequestAsync<T>(HttpMethod httpMethod, int id)
        {
            string newUri = string.Format("{0}/{1}", uri, id);
            using (var httpMessage = new HttpRequestMessage(httpMethod, newUri))
            {
                var response = await client.SendAsync(httpMessage);
                if (response.IsSuccessStatusCode && httpMethod != HttpMethod.Delete)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(data);
                }
            }
            return default(T);
        }

        public void ServiceRequest<T>(HttpMethod httpMethod, int? id, T t)
        {
            string newUri = (id == null ? uri : string.Format("{0}/{1}", uri, id));
            using (var httpMessage = new HttpRequestMessage(httpMethod, newUri))
            {
                httpMessage.Content = 
                    new StringContent(
                        JsonConvert.SerializeObject(t),
                        Encoding.UTF8,
                        "application/json");
                var response = client.SendAsync(httpMessage).Result;
            }
        }

        public async Task<bool> Exists(string code)
        {
            return false;
        }
    }
}
