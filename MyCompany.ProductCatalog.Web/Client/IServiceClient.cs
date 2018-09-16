using System.Net.Http;
using System.Threading.Tasks;

namespace MyCompany.ProductCatalog.Web.Client
{
    public interface IServiceClient
    {
        Task<T> ServiceRequestAsync<T>(HttpMethod httpMethod);
        Task<T> ServiceRequestAsync<T>(HttpMethod httpMethod, int id);
        void ServiceRequestAsync<T>(HttpMethod httpMethod, int? id, T t);
        Task<bool> Exists(string code);
    }
}
