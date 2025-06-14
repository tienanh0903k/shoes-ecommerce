namespace ShoesStoreApp.DAL.Models
{
    public class Brand
    {
        public Guid BrandId { get; set; }
        public string BrandName { get; set; }
        public string BrandImage { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; }
    }
}
