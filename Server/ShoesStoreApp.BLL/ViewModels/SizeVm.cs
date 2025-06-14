using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.ViewModels
{
    public class SizeVm
    {
        public Guid SizeId { get; set; }
        public string SizeName { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public Guid ProductId { get; set; }

    }
}
