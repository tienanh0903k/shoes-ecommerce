using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.ViewModels
{
    public class ProductVm
    {
        public Guid ProductId { get; set; }
        public Guid BrandId { get; set; }
        public string BrandName { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }


    }
}
