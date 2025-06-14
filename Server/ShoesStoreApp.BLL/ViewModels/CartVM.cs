namespace ShoesStoreApp.BLL.ViewModels;

public class CartVM
{
    public Guid CartId { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<CartItemVM> Items { get; set; }
}