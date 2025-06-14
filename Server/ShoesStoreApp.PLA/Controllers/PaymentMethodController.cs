using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesStoreApp.BLL.Services.CartService;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.PLA.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentMethodController : ControllerBase
{
    private readonly IPaymentMethodService _paymentMethodService;

    public PaymentMethodController(IPaymentMethodService paymentMethodService)
    {
        _paymentMethodService = paymentMethodService;
    }

        [Authorize]
        [HttpGet("get-all-payment-method")]
        public async Task<IActionResult> GetAll()
        {
            var payments = await _paymentMethodService.GetAllAsync();
            var paymentVM = new List<PaymentMethodVM>();
            foreach (var payment in payments)
            {
                paymentVM.Add(new PaymentMethodVM
                {
                    PaymentId = payment.PaymentId,
                    Description = payment.Description,
                    PaymentMethod = payment.PaymentMethod,
                });
            }
            return Ok(paymentVM);
        }

        [Authorize]
        [HttpGet("get-payment-method-by-id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var payment = await _paymentMethodService.GetByIdAsync(id);
            if(payment != null)
            {
                var paymentVM = new PaymentMethodVM()
                {
                    PaymentId = payment.PaymentId,
                    Description = payment.Description,
                    PaymentMethod = payment.PaymentMethod,
                };
                return Ok(paymentVM);
            }
            return BadRequest("The payment does not exist!");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add-new-payment-method")]
        public async Task<IActionResult> Add([FromBody] AddPaymentVM addPaymentVm)
        {
            var payment = new Payment()
            {
                Description = addPaymentVm.Description,
                PaymentMethod = addPaymentVm.PaymentMethod,
            };
            await _paymentMethodService.AddAsync(payment);
            return Ok(payment);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-payment-method/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AddPaymentVM addPaymentVm)
        {
            var payment = await _paymentMethodService.GetByIdAsync(id);
            if(payment != null)
            {
               payment.Description = addPaymentVm.Description;
               payment.PaymentMethod = addPaymentVm.PaymentMethod;

                await _paymentMethodService.UpdateAsync(payment);
                return Ok(payment);
            }
            return BadRequest("The payment does not exist!");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-payment-method/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var paymeny = await _paymentMethodService.GetByIdAsync(id);
            if(paymeny != null)
            {
                await _paymentMethodService.DeleteAsync(paymeny);
                return Ok(paymeny);
            }
            return BadRequest("Delete Faild!");
        }
}