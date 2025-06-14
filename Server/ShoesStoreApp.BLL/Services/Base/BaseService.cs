using ShoesStoreApp.DAL.Infrastructure;

namespace ShoesStoreApp.BLL.Services.Base
{
    public class BaseService<T>:IBaseService<T> where T : class
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<int> AddAsync(T entity)
        {
            if (entity != null)
            {
                _unitOfWork.GenericRepository<T>().Add(entity);

                return await _unitOfWork.SaveChangesAsync();
            }

            throw new ArgumentNullException(nameof(entity));
        }

        public virtual async Task<int> UpdateAsync(T entity)
        {
            if (entity != null)
            {
                _unitOfWork.GenericRepository<T>().Update(entity);

                return await _unitOfWork.SaveChangesAsync();
            }

            throw new ArgumentNullException(nameof(entity));
        }

        public virtual int Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                _unitOfWork.GenericRepository<T>().Delete(id);

                return _unitOfWork.SaveChanges();
            }

            throw new ArgumentNullException(nameof(id));
        }

        public virtual async Task<int> DeleteAsync(Guid id)
        {
            if (id != Guid.Empty)
            {
                _unitOfWork.GenericRepository<T>().Delete(id);

                return await _unitOfWork.SaveChangesAsync();
            }

            throw new ArgumentNullException(nameof(id));
        }

        public virtual async Task<int> DeleteAsync(T entity)
        {
            _unitOfWork.GenericRepository<T>().Delete(entity);

            return await _unitOfWork.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _unitOfWork.GenericRepository<T>().GetAllAsync();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.GenericRepository<T>().GetByIdAsync(id);
        }
    }
}
