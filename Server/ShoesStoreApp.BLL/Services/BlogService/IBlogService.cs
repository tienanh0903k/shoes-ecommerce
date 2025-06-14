using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.BlogService
{
    public interface IBlogService : IBaseService<Blog>
    {
        Task<PaginatedResult<BLogVm>> GetBLogListPaginationAsync(int pageIndex, int pageSize);
    }
}
