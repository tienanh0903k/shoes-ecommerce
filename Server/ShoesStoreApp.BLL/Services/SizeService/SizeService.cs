using Microsoft.EntityFrameworkCore;
using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Infrastructure;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.SizeService
{
    public class SizeService : BaseService<Size>, ISizeSrevice
    {
        public SizeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<Size>> GetSizeByProductId(Guid id)
        {
            var query = _unitOfWork.GenericRepository<Size>().GetQuery(s => s.ProductId == id);
            return await query.ToListAsync();
        }

        public async Task<Size> GetSizesByProductId(Guid id,string sizeName)
        {
            var query = _unitOfWork.GenericRepository<Size>().GetQuery().FirstOrDefault(s => s.ProductId == id && s.SizeName == sizeName);
            return  query;
        }
    }
}
