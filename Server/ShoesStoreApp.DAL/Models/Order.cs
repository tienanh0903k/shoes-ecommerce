namespace ShoesStoreApp.DAL.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid PaymentId { get; set; }
        public Payment Payment { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverPhone { get; set; }
        public string ReceiverAddress { get; set; }
        public decimal Total { get; set; }
        public bool IsPayment {get;set;}
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
