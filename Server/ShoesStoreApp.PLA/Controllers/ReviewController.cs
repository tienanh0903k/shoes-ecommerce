using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoesStoreApp.BLL.Services.ReviewService;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Models;
using System.Security.Claims;

namespace ShoesStoreApp.PLA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly UserManager<User> _userManager;

        public ReviewController(IReviewService reviewService, UserManager<User> userManager)
        {
            _reviewService = reviewService;
            _userManager = userManager;
        }


        [HttpGet("Get-Reviews/{productId}")]
        public async Task<IActionResult> GetReviewsByProductId(Guid productId)
        {
            var reviews = await _reviewService.GetReviewsByProductIdAsync(productId);
            var reviewVms = new List<ReviewVm>();

            if (reviews == null || !reviews.Any())
            {
                return NotFound(new { Message = "No reviews found for this product." });
            }
            foreach (var review in reviews)
            {
                var user = await _userManager.FindByIdAsync(review.UserId.ToString());
                reviewVms.Add(new ReviewVm
                {
                    ProductId = review.ProductId,
                    UserId = review.UserId,
                    FullName=user.FullName,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText,
                    CreatedDate = review.CreatedDate,
                    Status = review.Status,
                });
            }

            return Ok(reviewVms);
        }

        [HttpGet("Get-Review-By-Product-User/{productId}")]
        public async Task<IActionResult> GetReviewByProductUser(Guid productId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }
            Guid userId = Guid.Parse(userIdClaim);

            var review = await _reviewService.GetReviewByIdAsync(productId,userId);

            if (review == null)
            {
                return NotFound(new { Message = "Review not found." });
            }
            var user = await _userManager.FindByIdAsync(review.UserId.ToString());
            var reviewVm = new ReviewVm
            {
                ProductId = review.ProductId,
                UserId = review.UserId,
                FullName = user.FullName,
                Rating = review.Rating,
                ReviewText = review.ReviewText,
                CreatedDate = review.CreatedDate,
                Status = review.Status,
            };



            return Ok(reviewVm);
        }

        [Authorize]
        [HttpPost("Add-Review")]
        public async Task<IActionResult> AddReview([FromBody]AddReviewVm reviewVm)
        {

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }

            try
            {
                var review = new Review
                {
                    ProductId = reviewVm.ProductId,
                    UserId = Guid.Parse(userIdClaim),
                    Rating = reviewVm.Rating,
                    ReviewText = reviewVm.ReviewText,
                    CreatedDate = DateTime.UtcNow.ToLocalTime(),
                    Status = reviewVm.Status,
                };

                await _reviewService.AddAsync(review);

                return Ok(review);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("Delete-Review")]
        public async Task<IActionResult> DeleteReview(Guid productId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid userId = Guid.Parse(userIdClaim);
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }

            var review = await _reviewService.GetReviewByIdAsync(productId,userId);

            if (review != null)
            {
                await _reviewService.DeleteAsync(review);
                return Ok("Delete Succes");
            }

            return BadRequest("Delete Fail");

        }
    }
}
