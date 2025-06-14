using ShoesStoreApp.BLL.ViewModels.Payment;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.PaymentService;

public interface IPaymentService
{
    Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(Order model);
}