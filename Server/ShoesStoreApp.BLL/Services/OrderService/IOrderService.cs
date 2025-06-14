using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.CartService;

public interface IOrderService : IBaseService<Order>
{
    Task<List<OrderVM>> GetOrdersByPaymentStatus(Guid userId, bool isPaid);
}