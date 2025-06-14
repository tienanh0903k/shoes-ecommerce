using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Infrastructure;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.BlogService
{
    public class BlogService : BaseService<Blog>, IBlogService
    {
        public BlogService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<PaginatedResult<BLogVm>> GetBLogListPaginationAsync(int pageIndex, int pageSize)
        {
            var query = _unitOfWork.GenericRepository<Blog>().Get()
                .Select(blog => new BLogVm
                {
                    BlogId = blog.BlogId,
                    Title = blog.Title,
                    BlogImage = blog.BlogImage,
                    Description = blog.Description,
                    Detail = blog.Detail,
                    CreatedDate = blog.CreatedDate
                });

            return await PaginatedResult<BLogVm>.CreateAsync(query, pageIndex, pageSize);
        }
    }
}
