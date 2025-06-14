using Microsoft.AspNetCore.Http;
using ShoesStoreApp.DAL.Models;


namespace ShoesStoreApp.BLL.Services.Image
{
    public interface IImageService
    {
        Task<ImageSystem> SaveImageProductToDatabaseAsync(string fileName, string fileExtension);
        Task<ImageSystem> SaveImageBrandToDatabaseAsync(string fileName, string fileExtension);
        Task<ImageSystem> SaveImageBlogToDatabaseAsync(string fileName, string fileExtension);
        Task<ImageSystem> SaveAvatarToDatabaseAsync(string fileName, string fileExtension);

        void ValidateFileUpload(IFormFile file);
    }
}
