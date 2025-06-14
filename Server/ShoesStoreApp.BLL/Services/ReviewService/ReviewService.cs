using Microsoft.EntityFrameworkCore;
using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.DAL.Infrastructure;
using ShoesStoreApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesStoreApp.BLL.Services.ReviewService
{
    public class ReviewService:BaseService<Review>,IReviewService
    {
        public ReviewService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public async Task<IEnumerable<Review>> GetReviewsByProductIdAsync(Guid productId)
        {
            var query = _unitOfWork.GenericRepository<Review>().GetQuery(r => r.ProductId == productId);
            return await query.ToListAsync();
        }

        public async Task<Review> GetReviewByIdAsync(Guid productId,Guid userId)
        {
            var query = _unitOfWork.GenericRepository<Review>().GetQuery(r => r.ProductId == productId && r.UserId==userId);
            return await query.FirstOrDefaultAsync();
        }


        public async Task<bool> CanReviewProductAsync(Guid userId, Guid productId)
        {
            
            var orders = await _unitOfWork.GenericRepository<Order>()
                .GetQuery(o => o.UserId == userId && o.Status == "Giao hàng thành công")
                .Include(o => o.Items) 
                .ToListAsync();

            return orders.Any(order => order.Items.Any(item => item.ProductId == productId));
        }

        public override async Task<int> AddAsync(Review entity)
        {
            var canReview = await CanReviewProductAsync(entity.UserId, entity.ProductId);
            if (!canReview)
            {
                throw new UnauthorizedAccessException("You cannot review this product as you haven't purchased it or the order is not delivered.");
            }

            return await base.AddAsync(entity);
        }
    }
}
