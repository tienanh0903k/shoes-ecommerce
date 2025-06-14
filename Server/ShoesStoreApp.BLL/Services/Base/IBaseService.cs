namespace ShoesStoreApp.BLL.Services.Base
{
    public interface IBaseService<T> where T : class
    {
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        int Delete(Guid id);
        Task<int> DeleteAsync(Guid id);
        Task<int> DeleteAsync(T entity);

        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
