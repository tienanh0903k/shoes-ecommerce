namespace ShoesStoreApp.BLL.ViewModels;

public class AddOrderVM
{
    public Guid PaymentId { get; set; }
    public string ReceiverName { get; set; }
    public string ReceiverPhone { get; set; }
    public string ReceiverAddress { get; set; }
}