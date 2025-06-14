using Microsoft.EntityFrameworkCore;
using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Infrastructure;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.CartService;

public class CartService : BaseService<Cart>, ICartService
{
    public CartService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
    
    public async Task<CartVM> GetCartByUserId(Guid userId)
    {
        var cart = _unitOfWork.GenericRepository<Cart>()
            .GetQuery()
            .Include(c => c.Items)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefault(c => c.UserId == userId);

        if (cart == null) return null;
        
        return new CartVM
        {
            CartId = cart.CartId,
            CreatedDate = cart.CreatedDate,
            Items = cart.Items.Select(ci => new CartItemVM
            {
                CartId = ci.CartId,
                ProductId = ci.ProductId,
                ProductName = ci.Product.ProductName,
                ProductImage = ci.Product.ProductImage,
                Size = ci.Size,
                Price = ci.Price,
                Quantity = ci.Quantity
            }).ToList()
        };
    }
}