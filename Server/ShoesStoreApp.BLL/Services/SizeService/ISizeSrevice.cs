using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.SizeService
{
    public interface ISizeSrevice : IBaseService<Size>
    {
        Task<List<Size>> GetSizeByProductId(Guid id);
        Task<Size> GetSizesByProductId(Guid id, string sizeName);
    }
}
