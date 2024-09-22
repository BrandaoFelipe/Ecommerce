using CartAPI.DTOs;
using CartAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CartAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartRepository _cartRepository;

    public CartController(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    [HttpGet("getCart/{id}")]
    public async Task<ActionResult<CartDTO>> GetByUserId(string userId)
    {
        var cartDTO = await _cartRepository.GetCartByUserId(userId);

        if (cartDTO == null)
        {
            return NotFound();
        }

        return Ok(cartDTO);

    }

    [HttpPost("addCart")]
    public async Task<ActionResult<CartDTO>> AddCart(CartDTO cartDTO)
    {
        var cart = await _cartRepository.UpdateCartAsync(cartDTO);

        if (cart == null)
        {
            return NotFound();
        }

        return Ok(cart);
    }

    [HttpPut("UpdateCart")]
    public async Task<ActionResult<CartDTO>> UpdateCart(CartDTO cartDTO)
    {
        var cart = await _cartRepository.UpdateCartAsync(cartDTO);

        if (cart == null)
        {
            return NotFound();
        }

        return Ok(cart);
    }

    [HttpDelete("deleteCart/{id}")]
    public async Task<ActionResult<bool>> DeleteCart(int id)
    {
        var status = await _cartRepository.DeleteItemCartAsync(id);

        if (!status)
        {
            return BadRequest();
        }

        return Ok(status);
    }


}