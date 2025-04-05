using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using project.DTOs.Requests;
using project.DTOs.Responses;
using project.Migrations;
using project.Models;
using project.Services;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductServices productServices) : ControllerBase
    {
        
            private readonly IProductServices productServices = productServices;
            [HttpGet("")]
        public IActionResult GetAll()
        {
            var products = productServices.GetAll();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products.Adapt<IEnumerable<ProductResponse>>());
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var product = productServices.Get(c => c.Id == id);
            return product == null ? NotFound() : Ok(product.Adapt<ProductResponse>());
        }

        [HttpPost("")]
        public IActionResult create([FromForm] ProductRequest productRequest)
        {
            var productInDb = productServices.Add(productRequest.Adapt<Product>(), productRequest.mainImg);
            if (productInDb!=null)
            {
                return CreatedAtAction(nameof(GetById), new { productInDb.Id }, productInDb);
            }
            return BadRequest();
            
        }
        [HttpDelete("{id}")]
        public IActionResult remove([FromRoute] int id)
        {
            var productInDb = productServices.Remove(id);

            if (productInDb) {

                return NoContent();

            }
            return NotFound();
        }

    }
}
