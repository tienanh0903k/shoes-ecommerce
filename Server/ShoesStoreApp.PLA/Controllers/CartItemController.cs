using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesStoreApp.BLL.Services.CartService;
using ShoesStoreApp.BLL.Services.Custumer;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.PLA.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartItemController : ControllerBase
{
    private readonly ICartItemService _cartItemService;
    private readonly ICartService _cartService;

    public CartItemController(ICartItemService cartItemService, ICartService cartService)
    {
        _cartItemService = cartItemService;
        _cartService = cartService;
    }

    [Authorize]
    [HttpGet("get-cart-items/{cartId}")]
    public async Task<IActionResult> GetCartItemsByCartId(Guid cartId)
    {
        var cartItems = await _cartItemService.GetCartItemsByCartIdAsync(cartId);
        if (cartItems == null || !cartItems.Any())
        {
            return NotFound(new { Message = "No items found for the given CartId." });
        }

        return Ok(cartItems);
    }

    [Authorize]
    [HttpPost("add-cart-item")]
    public async Task<IActionResult> AddCartItem([FromBody] AddCartItem cartItem)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { Message = "User is not authenticated." });
        }
        
        var cartUser = await _cartService.GetCartByUserId(Guid.Parse(userId));
        
        var exitItem = cartUser.Items.FirstOrDefault(i => i.ProductId == cartItem.ProductId && i.Size == cartItem.Size);
        if (exitItem != null)
        {
            var itemCart = await _cartItemService.GetCartItemAsync(exitItem.CartId, exitItem.ProductId, exitItem.Size);
            itemCart.Quantity += cartItem.Quantity;
            itemCart.Size = exitItem.Size;

            await _cartItemService.AdjustSizeQuantity(cartItem.Size, cartItem.ProductId, cartItem.Quantity);
            await _cartItemService.UpdateAsync(itemCart);
            return Ok(itemCart);
        }
        
        var item = new CartItem()
        {
            CartId = cartUser.CartId,
            ProductId = cartItem.ProductId,
            Price = cartItem.Price,
            Quantity = cartItem.Quantity,
            Size = cartItem.Size
        };
        await _cartItemService.AdjustSizeQuantity(cartItem.Size, cartItem.ProductId, cartItem.Quantity);
        await _cartItemService.AddAsync(item);
        return Ok(item);
    }

    [Authorize]
    [HttpPut("update-cart-item/{cartId}/{productId}/{size}")]
    public async Task<IActionResult> UpdateCartItem(Guid cartId, Guid productId, string size, [FromBody] UpdateCartItemVM updateCartItem)
    {
        var cartItem = await _cartItemService.GetCartItemAsync(cartId, productId, size);
        if (cartItem == null)
            return NotFound("cartItem not found");

        int quantityDifference = updateCartItem.Quantity - cartItem.Quantity;

        if (quantityDifference > 0)
        {
            // Reduce quantity in stock if quantity increases
            await _cartItemService.AdjustSizeQuantity(size, productId, quantityDifference);
        }
        else if (quantityDifference < 0)
        {
            // Refund the quantity in stock if the quantity is reduced
            await _cartItemService.AdjustSizeQuantity(size, productId, quantityDifference );
        }


        cartItem.Quantity = updateCartItem.Quantity;
        cartItem.Price = updateCartItem.Price;
        cartItem.Size = updateCartItem.Size;
        
        await _cartItemService.UpdateAsync(cartItem);
        return Ok(cartItem);
    }

    [Authorize]
    [HttpDelete("remove-cart-item/{cartId}/{productId}/{size}")]
    public async Task<IActionResult> DeleteCartItem(Guid cartId, Guid productId, string size)
    {
        var cartItem = await _cartItemService.GetCartItemAsync(cartId, productId, size);
        if (cartItem == null)
            return NotFound("cartItem not found");

        await _cartItemService.AdjustSizeQuantity(size, productId, -cartItem.Quantity);

        await _cartItemService.DeleteAsync(cartItem);
        return Ok(cartItem);
    }
}