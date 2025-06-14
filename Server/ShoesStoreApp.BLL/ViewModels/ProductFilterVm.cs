namespace ShoesStoreApp.BLL.ViewModels
{
    public class ProductFilterVm
    {
        public string? Status { get; set; }
        public Guid? BrandId { get; set; }
        public string? SizeName { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
