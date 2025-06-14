using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.CartService;

public interface ICartService : IBaseService<Cart>
{
    Task<CartVM> GetCartByUserId(Guid userId);
}