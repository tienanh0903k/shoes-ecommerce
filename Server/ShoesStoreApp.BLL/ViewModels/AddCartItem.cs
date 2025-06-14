namespace ShoesStoreApp.BLL.ViewModels;

public class AddCartItem
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string Size { get; set; }
}