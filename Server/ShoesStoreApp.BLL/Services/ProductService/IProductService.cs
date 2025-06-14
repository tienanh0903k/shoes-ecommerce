using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.ProductService
{
    public interface IProductService:IBaseService<Product>
    {
        Task<PaginatedResult<ProductVm>> GetProductsByStatusAsync(string status, int pageIndex, int pageSize);
        Task<PaginatedResult<ProductVm>> GetProductsSimilarAsync(string status,Guid brandId, Guid productId, int pageIndex, int pageSize);
        Task<PaginatedResult<ProductVm>> GetFilteredProductsAsync(ProductFilterVm filter);
    }
}
