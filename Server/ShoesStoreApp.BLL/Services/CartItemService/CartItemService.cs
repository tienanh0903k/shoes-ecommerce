using Microsoft.EntityFrameworkCore;
using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.BLL.Services.SizeService;
using ShoesStoreApp.DAL.Infrastructure;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.CartService;

public class CartItemService : BaseService<CartItem>, ICartItemService
{
    private readonly ISizeSrevice _sizeService;
    public CartItemService(IUnitOfWork unitOfWork, ISizeSrevice sizeService) : base(unitOfWork)
    {
        _sizeService = sizeService;
    }

    public async Task<List<CartItem>> GetCartItemsByCartIdAsync(Guid cartId)
    {
        var cartItems = await _unitOfWork.GenericRepository<CartItem>()
            .GetQuery(ci => ci.CartId == cartId)
            .ToListAsync();

        return cartItems;
    }

    public Task<CartItem> GetCartItemAsync(Guid cartId, Guid productId, string Size)
    {
        var cartItem = _unitOfWork.GenericRepository<CartItem>()
            .GetQuery()
            .FirstOrDefaultAsync(ci => ci.ProductId == productId && ci.CartId == cartId && ci.Size == Size);
        return cartItem;
    }

    public async Task<bool> AdjustSizeQuantity(string sizeName, Guid productId, int adjustQuantity)
    {
        var size = await _unitOfWork.GenericRepository<Size>()
            .GetQuery(s => s.SizeName == sizeName && s.ProductId == productId)
            .FirstOrDefaultAsync();

        if (size == null || size.Quantity < adjustQuantity)
        {
            throw new InvalidOperationException("Not enough quantity in the stock");
        }

        return true;
    }
}