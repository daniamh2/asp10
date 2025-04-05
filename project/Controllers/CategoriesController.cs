using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project.Context;
using project.Models;
using project.Services;
using Mapster;
using project.DTOs.Requests;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryServices categoryServices) : ControllerBase
    {
        private readonly ICategoryServices categoryServices = categoryServices;

        [HttpGet("")]
        public IActionResult GetAll()
        {
            var categories = categoryServices.GetAll();
            return Ok(categories.Adapt<IEnumerable<CategoryResponse>>());
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var category = categoryServices.Get(c => c.Id == id);
            return category == null ? NotFound() : Ok(category.Adapt<IEnumerable<CategoryResponse>>());
        }

        [HttpPost("")]
        public IActionResult create([FromBody] CategoryRequest categoryRequest)
        {
            var categoryInDb = categoryServices.Add(categoryRequest.Adapt<Category>());
            return CreatedAtAction(nameof(GetById), new { categoryInDb.Id }, categoryInDb);
        }

        [HttpPut("{id}")]
        public IActionResult edit([FromRoute] int id, [FromBody] CategoryRequest categoryRequest)
        {
            var categoryInDb = categoryServices.Edit(id, categoryRequest.Adapt<Category>());

            if (!categoryInDb) return NotFound();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult remove([FromRoute] int id)
        {
            var categoryInDb = categoryServices.Remove(id);

            if (!categoryInDb) return NotFound();

            return NoContent();
        }
    }
}