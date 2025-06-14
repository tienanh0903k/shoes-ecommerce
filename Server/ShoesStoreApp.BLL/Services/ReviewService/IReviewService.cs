using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.ReviewService
{
    public interface IReviewService:IBaseService<Review>
    {
        Task<IEnumerable<Review>> GetReviewsByProductIdAsync(Guid productId);
        Task<Review> GetReviewByIdAsync(Guid productId,Guid userId);
        Task<bool> CanReviewProductAsync(Guid userId, Guid productId);
        Task<int> AddAsync(Review entity);
    }
}



