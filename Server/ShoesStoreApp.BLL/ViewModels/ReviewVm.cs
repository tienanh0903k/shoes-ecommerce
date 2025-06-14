using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.ViewModels
{
    public class ReviewVm
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
    }
}
