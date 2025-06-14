namespace ShoesStoreApp.DAL.Models
{
    public class Discount
    {
        public Guid DiscountId { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public decimal DiscountValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
    }
}
