using ShoesStoreApp.DAL.Data;
using ShoesStoreApp.DAL.Models;
using ShoesStoreApp.DAL.Repositories;

namespace ShoesStoreApp.DAL.Infrastructure
{
    public interface IUnitOfWork:IDisposable
    {
        ShoesStoreAppDbContext Context { get; }
        IGenericRepository<Brand> BrandRepository { get; }
        IGenericRepository<Blog> BlogRepository { get; }

        IGenericRepository<CartItem> CartItemRepository { get; }
        IGenericRepository<Cart> CartRepository { get; }


        IGenericRepository<Size> SizeRepository { get; }

        IGenericRepository<ImageSystem> ImageRepository { get; }
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<Review> ReviewRepository { get; }


        IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
