namespace ShoesStoreApp.BLL.ViewModels
{
    public class AddProductVm
    {
        public Guid BrandId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
