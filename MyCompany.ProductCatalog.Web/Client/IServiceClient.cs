using System.Net.Http;
using System.Threading.Tasks;

namespace MyCompany.ProductCatalog.Web.Client
{
    public interface IServiceClient
    {
        Task<T> ServiceRequestAsync<T>(HttpMethod httpMethod);
    }
}
