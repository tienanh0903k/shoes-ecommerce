using System.Text.Json.Serialization;

namespace ShoesStoreApp.DAL.Models
{
    public class OrderItem
    {
        public Guid OrderId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }
    }
}
