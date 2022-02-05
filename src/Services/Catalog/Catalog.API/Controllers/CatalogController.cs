using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {

        private readonly IProductRepository _reposiiroty;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository reposiiroty, ILogger<CatalogController> logger)
        {
            _reposiiroty = reposiiroty;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            var products = await _reposiiroty.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}",Name ="GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetPorductById(string id)
        {
            var product = await _reposiiroty.GetProduct(id);
            if (product == null) 
            {
                _logger.LogError($"Product with id: {id} not found .");
                return NotFound();
            }

            return Ok(product);

        }


        [Route("[action]/{category}", Name = "GetProductCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetProductCategory(string category)
        {
            var products = await _reposiiroty.GetProductByCategory(category);
            return Ok(products);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateProduct([FromBody] Product product)
        {
            await _reposiiroty.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }


        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _reposiiroty.UpdateProduct(product));
        }


        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteProductById(string id)
        {
            return Ok(await _reposiiroty.DeleteProduct(id));
        }





    }
}
