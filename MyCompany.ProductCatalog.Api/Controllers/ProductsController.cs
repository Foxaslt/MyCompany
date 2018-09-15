using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyCompany.ProductCatalog.Api.Database;
using MyCompany.ProductCatalog.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using Validation;

namespace MyCompany.ProductCatalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DatabaseContext Context;
        private readonly ILogger<ProductsController> logger;

        public ProductsController(DatabaseContext dbContext, ILogger<ProductsController> logger)
        {
            Requires.NotNull(dbContext, nameof(dbContext));
            Requires.NotNull(logger, nameof(logger));

            this.Context = dbContext;
            this.logger = logger;
        }

        // GET api/products
        [HttpGet]
        [ProducesResponseType(500)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            try
            {
                var products = await Context.Products.ToListAsync();

                return Ok(products);
            }
            catch (System.Exception ex)
            {
                logger.LogError(LoggingEvents.GetItemException, ex, "Get() exception");
                return StatusCode(500);
            }
        }

        // GET api/products/5
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(200, Type = typeof(Product))]
        public async Task<ActionResult<Product>> Get(int id)
        {
            try
            {
                var product = await Context.Products.FirstOrDefaultAsync(prod => prod.Id.Equals(id));
                if (product == null)
                    return NotFound();

                return Ok(product);
            }
            catch (System.Exception ex)
            {
                logger.LogError(LoggingEvents.GetItemWithIdException, ex, "Get({Id}) exception", id);
                return StatusCode(500);
            }
        }

        // POST api/products
        [HttpPost]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Post([FromBody] Product newProduct)
        {
            try
            {
                Requires.NotNull(newProduct, nameof(newProduct));

                await Context.Products.AddAsync(newProduct);
                await Context.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                logger.LogError(LoggingEvents.SaveItemException, ex, "Post() exception");
                return StatusCode(500);
            }
        }

        // PUT api/products/5
        [HttpPut("{id}")]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Put(int id, [FromBody] Product updatedProduct)
        {
            try
            {
                Requires.NotNull(updatedProduct, nameof(updatedProduct));

                var product = await Context.Products.FirstOrDefaultAsync(prod => prod.Id.Equals(id));
                if (product != null)
                {
                    product.Code = updatedProduct.Code;
                    product.Name = updatedProduct.Name;
                    product.Photo = updatedProduct.Photo;
                    product.Price = updatedProduct.Price;
                    await Context.SaveChangesAsync();
                }

                return Ok();
            }
            catch (System.Exception ex)
            {
                logger.LogError(LoggingEvents.UpdateItemException, ex, "Put({Id}) exception", id);
                return StatusCode(500);
            }
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var product = await Context.Products.FirstOrDefaultAsync(prod => prod.Id.Equals(id));
                if (product != null)
                {
                    Context.Products.Remove(product);
                    await Context.SaveChangesAsync();
                }
                return Ok();
            }
            catch (System.Exception ex)
            {
                logger.LogError(LoggingEvents.DeleteItemException, ex, "Delete({Id}) exception", id);
                return StatusCode(500);
            }
        }
    }
}
