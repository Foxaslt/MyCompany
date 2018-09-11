using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCompany.ProductCatalog.Domain;
using MyCompany.ProductCatalog.Web.Client;
using System.Collections.Generic;
using System.Net.Http;

namespace MyCompany.ProductCatalog.Web.Models
{
    public class ProductsModel : PageModel
    {
        private IServiceClient client;

        public ProductsModel(IServiceClient client)
        {
            this.client = client;
        }

        public List<Product> Products { get; set; }

        public async void OnGet()
        {
            Products = await client.ServiceRequestAsync<List<Product>>(HttpMethod.Get);
        }
    }
}
