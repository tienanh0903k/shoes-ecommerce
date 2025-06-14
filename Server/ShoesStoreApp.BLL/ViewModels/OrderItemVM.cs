namespace ShoesStoreApp.BLL.ViewModels.Payment;

public class OrderItemVM
{
    public string ProductImage { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string Size { get; set; }
}