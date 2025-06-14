using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.CartService;

public interface ICartItemService : IBaseService<CartItem>
{
    Task<List<CartItem>> GetCartItemsByCartIdAsync(Guid cartId);
    Task<CartItem> GetCartItemAsync(Guid cartId, Guid productId, string Size);
    Task<bool> AdjustSizeQuantity(string sizeName, Guid productId, int adjustQuantity);
}