using Microsoft.EntityFrameworkCore;
using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Infrastructure;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.ProductService
{
    public class ProductService:BaseService<Product>, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork):base(unitOfWork) {
            
        }

        public async Task<PaginatedResult<ProductVm>> GetProductsByStatusAsync(string status, int pageIndex, int pageSize)
        {
            var query = _unitOfWork.GenericRepository<Product>().GetQuery(p => p.Status == status).Include(p => p.Brand)
                .Select(product => new ProductVm
                {
                    ProductId = product.ProductId,
                    BrandId = product.BrandId,
                    BrandName = product.Brand.BrandName,
                    ProductName = product.ProductName,
                    ProductImage = product.ProductImage,
                    Price = product.Price,
                    Description = product.Description,
                    Status = product.Status,
                });

            return await PaginatedResult<ProductVm>.CreateAsync(query, pageIndex, pageSize);
        }

        public async Task<PaginatedResult<ProductVm>> GetProductsSimilarAsync(string status,Guid brandId, Guid productId, int pageIndex, int pageSize)
        {
            var query = _unitOfWork.GenericRepository<Product>().GetQuery(p => p.Status == status && p.BrandId==brandId && p.ProductId!= productId).Include(p => p.Brand)
                .Select(product => new ProductVm
                {
                    ProductId = product.ProductId,
                    BrandId = product.BrandId,
                    BrandName = product.Brand.BrandName,
                    ProductName = product.ProductName,
                    ProductImage = product.ProductImage,
                    Price = product.Price,
                    Description = product.Description,
                    Status = product.Status,
                });
            return await PaginatedResult<ProductVm>.CreateAsync(query, pageIndex, pageSize);
        }


        public async Task<PaginatedResult<ProductVm>> GetFilteredProductsAsync(ProductFilterVm filter)
        {
            IQueryable<Product> query = _unitOfWork.GenericRepository<Product>().GetQuery().Include(p => p.Brand);


            // Filter by status
            if (!string.IsNullOrEmpty(filter.Status))
            {
                query = query.Where(p => p.Status == filter.Status);
            }

            // Filter by brand
            if (filter.BrandId.HasValue)
            {
                query = query.Where(p => p.BrandId == filter.BrandId);
            }

            // Filter by size
            if (!string.IsNullOrEmpty(filter.SizeName))
            {
                query = query.Where(p => p.Sizes.Any(s => s.SizeName == filter.SizeName));
            }

            // Filter by price
            if (filter.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= filter.MinPrice.Value);
            }
            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);
            }



            var queryVm= query.Select(product => new ProductVm
            {
                ProductId = product.ProductId,
                BrandId = product.BrandId,
                BrandName = product.Brand.BrandName,
                ProductName = product.ProductName,
                ProductImage = product.ProductImage,
                Price = product.Price,
                Description = product.Description,
                Status = product.Status,
            });

            return await PaginatedResult<ProductVm>.CreateAsync(queryVm, filter.PageIndex, filter.PageSize);
        }
    }
}
