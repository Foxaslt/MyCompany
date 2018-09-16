using Microsoft.AspNetCore.Mvc;
using MyCompany.ProductCatalog.Domain;
using MyCompany.ProductCatalog.Web.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product, IFormFile imageData)
        {
            var img = Common.ImageToBytes(imageData);
            if (img != null)
                product.Photo = img;

            client.ServiceRequest(HttpMethod.Post, null, product);
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
        public async Task<IActionResult> Edit(int id, Product product, IFormFile imageData)
        {
            if (ModelState.IsValid)
            {
                var img = Common.ImageToBytes(imageData);
                if (img != null)
                    product.Photo = img;

                client.ServiceRequest(HttpMethod.Put, id, product);
            }
            return RedirectToAction(nameof(Index));
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> VerifyCode(string code)
        {
            bool exists = await client.Exists(code);
            if (exists)
                return Json(data: false);
            else
                return Json(data: true);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await client.ServiceRequestAsync<Product>(HttpMethod.Delete, id);
            return RedirectToAction(nameof(Index));
        }
    }
}