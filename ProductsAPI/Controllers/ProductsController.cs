using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.DTOs;
using ProductsAPI.Roles;
using ProductsAPI.Services;

namespace ProductsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductService _repository;

    public ProductsController(IProductService repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll()
    {
        var product = await _repository.GetAll();

        if (product == null)
        {
            return NotFound("Product is empty");
        }

        return Ok(product);
    }

    [HttpGet("GetByCategories")]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetByCategories(int id)
    {
        var products = await _repository.GetByCategories(id);

        if (products == null)
        {
            return NotFound($"Id {id} not found");
        }

        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDTO>> Get(int id)
    {
        var product = await _repository.GetById(id);

        if (product == null)
        {
            return NotFound($"Id {id} not found");
        }

        return Ok(product);
    }

    [HttpPost]
    [Authorize(Roles = Role.Admin)]
    public async Task<ActionResult<ProductDTO>> Create([FromBody] ProductDTO productDTO)
    {
        if (productDTO == null)
        {
            return BadRequest("Product cannot be null");
        }
        await _repository.AddProduct(productDTO);

        return CreatedAtAction("Get", new { id = productDTO.Id }, productDTO);
    }

    [HttpPut]
    public async Task<ActionResult<ProductDTO>> Update([FromBody] ProductDTO productDTO)
    {
        if (productDTO == null)
        {
            return BadRequest("Invalid Data");
        }

        await _repository.UpdateProduct(productDTO);

        return Ok(productDTO);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = Role.Admin)]
    public async Task<ActionResult<ProductDTO>> Delete(int id)
    {
        var produtoDto = await _repository.GetById(id);

        if (produtoDto == null)
        {
            return NotFound("Product not found");
        }

        await _repository.DeleteProduct(id);

        return Ok(produtoDto);
    }
}
