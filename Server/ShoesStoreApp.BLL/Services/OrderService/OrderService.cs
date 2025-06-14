using Microsoft.EntityFrameworkCore;
using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.BLL.ViewModels.Payment;
using ShoesStoreApp.DAL.Infrastructure;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.CartService;

public class OrderService : BaseService<Order>, IOrderService
{
    public OrderService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<List<OrderVM>> GetOrdersByPaymentStatus(Guid userId, bool isPaid)
    {
        var order = _unitOfWork.GenericRepository<Order>()     
            .GetQuery()
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .Where(o => o.UserId == userId )
            .ToList();

        return order.Select(o => new OrderVM
        {
            OrderId = o.OrderId,
            UserId = o.UserId,
            CreatedDate = o.CreatedDate,
            ReceiverPhone = o.ReceiverPhone,
            ReceiverName = o.ReceiverName,
            PaymentId = o.PaymentId,
            ReceiverAddress = o.ReceiverAddress,
            Total = o.Total,
            Status = o.Status,
            IsPayment = o.IsPayment,
            Items = o.Items.Select(i => new OrderItemVM 
            {
                ProductImage = i.Product.ProductImage,
                ProductName = i.Product.ProductName,
                Size = i.Size,
                Price = i.Price,
                Quantity = i.Quantity,
            }).ToList()
        }).ToList();
    }
}