using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project.Context;
using project.Models;
using project.Services;
using Mapster;
using project.DTOs.Requests;
using project.Migrations;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController(IBrandServices brandServices) : ControllerBase
    {
        private readonly IBrandServices brandServices = brandServices;

        [HttpGet("")]
        public IActionResult GetAll()
        {
            var brands = brandServices.GetAll();
            return Ok(brands.Adapt<IEnumerable<BrandResponse>>());
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var brand = brandServices.Get(c => c.Id == id);
            return brand == null ? NotFound() : Ok(brand.Adapt<BrandResponse>());
        }

        [HttpPost("")]
        public IActionResult create([FromBody] BrandRequest brandRequest)
        {
            var brandInDb = brandServices.Add(brandRequest.Adapt<Brand>());
            return CreatedAtAction(nameof(GetById), new { brandInDb.Id }, brandInDb);
        }

        [HttpPut("{id}")]
        public IActionResult edit([FromRoute] int id, [FromBody] BrandRequest brandRequest)
        {
            var brandInDb = brandServices.Edit(id, brandRequest.Adapt<Brand>());

            if (!brandInDb) return NotFound();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult remove([FromRoute] int id)
        {
            var brandInDb = brandServices.Remove(id);

            if (!brandInDb) return NotFound();

            return NoContent();
        }
    }
}