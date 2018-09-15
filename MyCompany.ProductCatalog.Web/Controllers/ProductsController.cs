using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCompany.ProductCatalog.Domain;
using MyCompany.ProductCatalog.Web.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyCompany.ProductCatalog.Web.Controllers
{
    public class ProductsController : Controller
    {
        private IServiceClient client;
        public ProductsController(IServiceClient client)
        {
            this.client = client;
        }

        public async Task<IActionResult> Index()
        {
            var products = await client.ServiceRequestAsync<List<Product>>(HttpMethod.Get);
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await client.ServiceRequestAsync<Product>(HttpMethod.Get, id);
            return View(product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Product product)
        //{
        //    client.ServiceRequestAsync(HttpMethod.Post, null, product);
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product, IList<IFormFile> files)
        {
            client.ServiceRequestAsync(HttpMethod.Post, null, product);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await client.ServiceRequestAsync<Product>(HttpMethod.Get, id);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (Exists(id, product.Code))
                ModelState.AddModelError("Id", "Id already exist");

            client.ServiceRequestAsync(HttpMethod.Put, id, product);
            return RedirectToAction(nameof(Index));
        }

        private bool Exists(int id, string code)
        {
            return false;
        }

        public async Task<IActionResult> Delete(int id)
        {
            await client.ServiceRequestAsync<Product>(HttpMethod.Delete, id);
            return RedirectToAction(nameof(Index));
        }
    }
}