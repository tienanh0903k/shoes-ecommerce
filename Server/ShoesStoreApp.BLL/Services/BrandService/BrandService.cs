using Microsoft.EntityFrameworkCore;
using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Infrastructure;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.BrandService
{
    public class BrandService : BaseService<Brand>, IBrandService
    {
        public BrandService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<PaginatedResult<BrandVm>> GetBrandListPaginationAsync(int pageIndex, int pageSize)
        {
            var query = _unitOfWork.GenericRepository<Brand>().Get()
                .Select(brand => new BrandVm
                {
                    BrandId = brand.BrandId,
                    BrandName = brand.BrandName,
                    BrandImage = brand.BrandImage,
                    Description = brand.Description
                });
            return await PaginatedResult<BrandVm>.CreateAsync(query,pageIndex, pageSize);
        }
    }
}
