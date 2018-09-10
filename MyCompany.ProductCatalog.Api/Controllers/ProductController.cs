using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCompany.ProductCatalog.Api.Database;
using MyCompany.ProductCatalog.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCompany.ProductCatalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DatabaseContext db;
        public ProductsController(DatabaseContext db)
        {
            this.db = db;
        }

        // GET api/values
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await db.Products.ToListAsync();

            return Ok(products);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(Product))]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await db.Products.FirstOrDefaultAsync(prod => prod.Id.Equals(id));
            if (product != null)
                return Ok(product);
            else
                return NotFound();
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromBody] Product newProduct)
        {
            await db.Products.AddAsync(newProduct);
            await db.SaveChangesAsync();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody] Product updatedProduct)
        {
            var product = await db.Products.FirstOrDefaultAsync(prod => prod.Id.Equals(id));
            if (product != null)
            {
                product.Code = updatedProduct.Code;
                product.Name = updatedProduct.Name;
                product.Photo = updatedProduct.Photo;
                product.Price = updatedProduct.Price;
                await db.SaveChangesAsync();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            var product = await db.Products.FirstOrDefaultAsync(prod => prod.Id.Equals(id));
            db.Products.Remove(product);
            await db.SaveChangesAsync();
        }
    }
}
