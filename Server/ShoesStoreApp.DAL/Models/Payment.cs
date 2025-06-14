namespace ShoesStoreApp.DAL.Models
{
    public class Payment
    {
        public Guid PaymentId { get; set; }
        public string PaymentMethod { get; set; }
        public string Description { get; set; }
        public List<Order> Orders { get; set; }
    }
}
