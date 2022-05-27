using Basket.Application.Common.Models.Responses;
using Basket.Application.Services.ShoppingCarts.AddShoppingCartItem;
using Basket.Application.Services.ShoppingCarts.ClearShoppingCartItem;
using Basket.Application.Services.ShoppingCarts.DeleteShoppingCartItem;
using Basket.Application.Services.ShoppingCarts.GetShoppingCartById;
using Basket.Application.Services.ShoppingCarts.UpdateProductInShoppingCartItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers;

[Route("api/[controller]")]
public class ShoppingCartController : ControllerBase
{
    private readonly ISender _mediatr;

    public ShoppingCartController(ISender mediatr)
    {
        _mediatr = mediatr;
    }
    
    /// <summary>
    /// Returning shopping cart by its id
    /// </summary>
    /// <param name="id">Shopping cart id</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShoppingCartResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<ShoppingCartResponse> Get(Guid id)
    {
        return _mediatr.Send(new GetShoppingCartByIdQuery(id));
    }
    
    /// <summary>
    /// Add new item to shopping cart
    /// </summary>
    /// <param name="command">Data of shopping cart item</param>
    /// <returns></returns>
    [HttpPost("item")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddShoppingCartItem([FromBody]AddShoppingCartItemCommand command)
    {
        var shoppingCartId = await _mediatr.Send(command);

        return Ok();
    }
    
    /// <summary>
    /// Clear user shopping cart
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Clear(
        [FromBody]ClearShoppingCartItemCommand command)
    {
        await _mediatr.Send(command);

        return NoContent();
    }
    
    /// <summary>
    /// Delete shopping cart item
    /// </summary>
    /// <param name="command">Delete shopping cart item data</param>
    /// <returns></returns>
    [HttpDelete("item")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteShoppingCartItem(
        [FromBody]DeleteShoppingCartItemCommand command)
    {
        await _mediatr.Send(command);

        return NoContent();
    }
    
    /// <summary>
    /// Update product in shopping cart item
    /// </summary>
    /// <param name="command">Update product in shopping cart item data</param>
    /// <returns></returns>
    [HttpPut("item/product")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProductInShoppingCartItem(
        [FromBody]UpdateProductInShoppingCartItemCommand command)
    {
        await _mediatr.Send(command);

        return CreatedAtAction(
            nameof(Get), 
            new { Id = command.ShoppingCartId }, 
            command);
    }
}
