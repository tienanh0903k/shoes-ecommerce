using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.ViewModels
{
    public class AddReviewVm
    {
        public Guid ProductId { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public string Status { get; set; }
    }
}
