using Microsoft.EntityFrameworkCore;
using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.DAL.Infrastructure;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.CartService;

public class PaymentMethodService : BaseService<Payment>, IPaymentMethodService
{
    public PaymentMethodService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
    
}