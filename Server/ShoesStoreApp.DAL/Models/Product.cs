using System.Text.Json.Serialization;

namespace ShoesStoreApp.DAL.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public List<Size> Sizes { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Discount> Discounts { get; set; }
        [JsonIgnore]
        public List<OrderItem> Items { get; set; }  
        [JsonIgnore]
        public List<CartItem> CartItems { get; set; }

    }
}
