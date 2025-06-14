namespace ShoesStoreApp.BLL.ViewModels;

public class CartItemVM
{
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductImage { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string Size { get; set; }
}