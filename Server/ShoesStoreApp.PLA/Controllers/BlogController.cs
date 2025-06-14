using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesStoreApp.BLL.Services.BlogService;
using ShoesStoreApp.BLL.Services.Image;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.PLA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IImageService _imageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BlogController(IBlogService blogService, IImageService imageService, IWebHostEnvironment webHostEnvironment)
        {
            _blogService = blogService;
            _imageService = imageService;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Upload-Image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded or file is empty.");
                }

                _imageService.ValidateFileUpload(file);

                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var fileExtension = Path.GetExtension(file.FileName).ToLower();
                var localPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "Blog", $"{fileName}{fileExtension}");

                using (var stream = new FileStream(localPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var uploadedImage = await _imageService.SaveImageBlogToDatabaseAsync(fileName, fileExtension);

                return Ok(uploadedImage);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("get-all-blog")]
        public async Task<IActionResult> GetAllBlog()
        {
            var blogs = await _blogService.GetAllAsync();
            var blogVm = new List<BLogVm>();
            foreach (var blog in blogs)
            {
                blogVm.Add(new BLogVm
                {
                    BlogId = blog.BlogId,
                    Title = blog.Title,
                    BlogImage = blog.BlogImage,
                    Description = blog.Description,
                    Detail = blog.Detail,
                    CreatedDate = blog.CreatedDate
                });
            }
            return Ok(blogVm);
        }

        [HttpGet("get-blog-by-id/{id}")]
        public async Task<IActionResult> GetBlogById(Guid id)
        {
            var blog = await _blogService.GetByIdAsync(id);
            if(blog != null)
            {
                var blogVm = new BLogVm()
                {
                    BlogId = blog.BlogId,
                    Title = blog.Title,
                    BlogImage = blog.BlogImage,
                    Description = blog.Description,
                    Detail = blog.Detail,
                    CreatedDate = blog.CreatedDate
                };
                return Ok(blogVm);
            }
            return BadRequest("The blog does not exist!");
        }

        [HttpGet("Get-All-Blog-Pagination")]
        public async Task<IActionResult> GetAllBlogPagination([FromQuery] int pageIndex, int pageSize)
        {
            if (pageIndex <= 0 || pageSize <= 0)
            {
                return BadRequest(new { message = "PageIndex and PageSize must be greater than 0." });
            }

            var blogs = await _blogService.GetBLogListPaginationAsync(pageIndex, pageSize);
            return Ok(blogs);

        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add-new-blog")]
        public async Task<IActionResult> AddNewBlog([FromBody] AddBlogVm addBlogVm)
        {
            var blog = new Blog()
            {
                BlogId = Guid.NewGuid(),
                Title = addBlogVm.Title,
                BlogImage = addBlogVm.BlogImage,
                Description = addBlogVm.Description,
                Detail = addBlogVm.Detail,
                CreatedDate = DateTime.Now,
            };
            await _blogService.AddAsync(blog);
            return Ok(blog);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-blog/{id}")]
        public async Task<IActionResult> UpdateBlog(Guid id, [FromBody] AddBlogVm addBlogVm)
        {
            var blog = await _blogService.GetByIdAsync(id);
            if(blog != null)
            {
                blog.Title = addBlogVm.Title;
                blog.BlogImage = addBlogVm.BlogImage;
                blog.Description = addBlogVm.Description;
                blog.Detail = addBlogVm.Detail;

                await _blogService.UpdateAsync(blog);
                return Ok(blog);
            }
            return BadRequest("The blog does not exist!");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-blog/{id}")]
        public async Task<IActionResult> DeleteBlog(Guid id)
        {
            var blog = await _blogService.GetByIdAsync(id);
            if(blog != null)
            {
                await _blogService.DeleteAsync(blog);
                return Ok(blog);
            }
            return BadRequest("Delete Faild!");
        }

    }
}
