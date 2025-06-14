using ShoesStoreApp.BLL.ViewModels.Payment;

namespace ShoesStoreApp.BLL.ViewModels;

public class OrderVM
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public Guid PaymentId { get; set; }
    public string ReceiverName { get; set; }
    public string ReceiverPhone { get; set; }
    public string ReceiverAddress { get; set; }
    public decimal Total { get; set; }
    public bool IsPayment {get;set;}
    public string Status { get; set; }
    public DateTime CreatedDate { get; set; }

    public List<OrderItemVM> Items { get; set; }
}