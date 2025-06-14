using Microsoft.EntityFrameworkCore;
using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.DAL.Infrastructure;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.CartService;

public class OrderItemService : BaseService<OrderItem>, IOrderItemService
{
    public OrderItemService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
    
}