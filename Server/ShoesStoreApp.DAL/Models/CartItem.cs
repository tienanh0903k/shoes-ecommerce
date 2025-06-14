using System.Text.Json.Serialization;

namespace ShoesStoreApp.DAL.Models
{
    public class CartItem
    {
        public Guid CartId { get; set; }
        [JsonIgnore]
        public Cart Cart { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }
    }
}
