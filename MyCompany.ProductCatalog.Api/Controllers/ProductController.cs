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
        private readonly DatabaseContext Context;

        public ProductsController(DatabaseContext dbContext)
        {
            this.Context = dbContext;
        }

        // GET api/values
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await Context.Products.ToListAsync();

            return Ok(products);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(Product))]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await Context.Products.FirstOrDefaultAsync(prod => prod.Id.Equals(id));
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromBody] Product newProduct)
        {
            await Context.Products.AddAsync(newProduct);
            await Context.SaveChangesAsync();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody] Product updatedProduct)
        {
            var product = await Context.Products.FirstOrDefaultAsync(prod => prod.Id.Equals(id));
            if (product != null)
            {
                product.Code = updatedProduct.Code;
                product.Name = updatedProduct.Name;
                product.Photo = updatedProduct.Photo;
                product.Price = updatedProduct.Price;
                await Context.SaveChangesAsync();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            var product = await Context.Products.FirstOrDefaultAsync(prod => prod.Id.Equals(id));
            Context.Products.Remove(product);
            await Context.SaveChangesAsync();
        }
    }
}
