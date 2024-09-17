using Microsoft.AspNetCore.Mvc;
using ProductsAPI.DTOs;
using ProductsAPI.Services;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _repository;

        public CategoryController(ICategoryService repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll()
        {
            var categories = await _repository.GetAllCategories();

            if (categories == null)
            {
                return NotFound("Not Found!");
            }            
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var category = await _repository.GetCategoryById(id);

            if (category == null)
            {
                return NotFound($"Id {id} not found");
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Create([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                return BadRequest("Category cannot be null");
            }
            await _repository.AddCategory(categoryDTO);

            return CreatedAtAction("Get", new { id = categoryDTO.CategoryId }, categoryDTO);
        }

        [HttpPut]
        public async Task<ActionResult<CategoryDTO>> Update(int id, [FromBody] CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.CategoryId)
            {
                return BadRequest($"Id {id} is not valid.");
            }

            await _repository.UpdateCategory(categoryDTO);

            return Ok(categoryDTO);
        }

        [HttpDelete]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var product = await _repository.GetCategoryById(id);

            await _repository.DeleteCategory(id);
            
            return Ok(product);
        }
    }
}
