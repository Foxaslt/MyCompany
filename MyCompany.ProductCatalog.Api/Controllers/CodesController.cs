using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCompany.ProductCatalog.Api.Database;
using Validation;

namespace MyCompany.ProductCatalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodesController : ControllerBase
    {
        private readonly DatabaseContext Context;
        private readonly ILogger<CodesController> logger;

        public CodesController(DatabaseContext dbContext, ILogger<CodesController> logger)
        {
            this.Context = dbContext;
            this.logger = logger;
        }

        [HttpGet("{code}")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public ActionResult<bool> Get(string code)
        {
            Requires.NotNull(code, nameof(code));

            return Ok(Context.Products.Any(product => product.Code.Equals(code)));
        }
    }
}