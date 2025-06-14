namespace ShoesStoreApp.DAL.Models
{
    public class Size
    {
        public Guid SizeId { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public string SizeName { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }  
    }
}
