using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesStoreApp.BLL.Services.CartService;
using ShoesStoreApp.BLL.Services.Custumer;
using ShoesStoreApp.BLL.Services.PaymentService;
using ShoesStoreApp.BLL.Services.SizeService;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.BLL.ViewModels.Payment;
using ShoesStoreApp.DAL.Data;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.PLA.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ICartService _cartService;
    private readonly IUserService _userService;
    private readonly ICartItemService _cartItemService;
    private readonly IOrderItemService _orderItemService;
    private readonly ISizeSrevice _sizeService;
    private readonly IPaymentService _paymentService;
    private readonly ShoesStoreAppDbContext _context;

    public OrderController(
        IOrderService orderService, 
        ICartService cartService,
        ICartItemService cartItemService,
        IUserService userService, 
        IOrderItemService orderItemService,
        IPaymentService paymentService, ISizeSrevice sizeService,
    ShoesStoreAppDbContext context)
    {
        _orderService = orderService;
        _cartService = cartService;
        _cartItemService = cartItemService;
        _userService = userService;
        _orderItemService = orderItemService;
        _paymentService = paymentService;
        _sizeService = sizeService;
        _context = context;
    }

    [Authorize]
    [HttpPost("add-new-order")]
    public async Task<IActionResult> AddNewOrder([FromBody] AddOrderVM addOrderVM)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) 
            return Unauthorized();
        
        var user = await  _userService.getUserByEmail(userId);
        var cartUser = await _cartService.GetCartByUserId(user.Id);
        decimal totalPrice = 0;

        // Bắt đầu transaction
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                // Add order
                var order = new Order()
                {
                    UserId = user.Id,
                    PaymentId = addOrderVM.PaymentId,
                    ReceiverName = addOrderVM.ReceiverName,
                    ReceiverAddress = addOrderVM.ReceiverAddress,
                    ReceiverPhone = addOrderVM.ReceiverPhone,
                    IsPayment = false,
                    Status = "Pending",
                };
                await _orderService.AddAsync(order);

                // Add item in order
                foreach (var item in cartUser.Items)
                {
                    var orderItem = new OrderItem()
                    {
                        OrderId = order.OrderId,
                        ProductId = item.ProductId,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        Size = item.Size,
                    };
                    await _orderItemService.AddAsync(orderItem);

                    // Calculate total price
                    var price = orderItem.Price * item.Quantity;
                    totalPrice += price;

                    // Delete item in cart
                    var cartItem = await _cartItemService.GetCartItemAsync(item.CartId, item.ProductId, item.Size);
                    await _cartItemService.DeleteAsync(cartItem);

                    var sizes=await _sizeService.GetSizesByProductId(item.ProductId,item.Size);
                    sizes.Quantity -= item.Quantity;
                    await _sizeService.UpdateAsync(sizes);
                }

                // Update total price for the order
                order.Total = totalPrice;
                await _orderService.UpdateAsync(order);

                // URL Payment
                var response = await _paymentService.CreatePaymentAsync(order);

                // Nếu không có lỗi, commit transaction
                await transaction.CommitAsync();

                var payURL = new MomoCreatePaymentResponseModel()
                {
                    PayUrl = response.PayUrl,
                };
                return Ok(payURL);
            }
            catch (Exception ex)
            {
                // Nếu có lỗi, rollback transaction
                await transaction.RollbackAsync();

                return StatusCode(500, new { message = "An error occurred while processing the order", error = ex.Message });
            }
        }
    }

    [Authorize]
    [HttpGet("get-all-unpaid-order-of-user")]
    public async Task<IActionResult> GetAllUnpaidOrder()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { Message = "User is not authenticated." });
        }

        var order = _orderService.GetOrdersByPaymentStatus(Guid.Parse(userId), false);
        return Ok(order.Result);
    }
}