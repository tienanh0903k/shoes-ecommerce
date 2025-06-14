using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.BrandService
{
    public interface IBrandService : IBaseService<Brand>
    {
        Task<PaginatedResult<BrandVm>> GetBrandListPaginationAsync(int pageIndex, int pageSize);
    }
}
