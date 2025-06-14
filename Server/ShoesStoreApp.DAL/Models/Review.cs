namespace ShoesStoreApp.DAL.Models
{
    public class Review
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
    }
}
