namespace ShoesStoreApp.DAL.Models
{
    public class Cart
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public List<CartItem> Items { get; set; }
    }
}
