using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.BLL.Services.BrandService;
using ShoesStoreApp.BLL.Services.Image;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.PLA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brand;
        private readonly IImageService _imageService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BrandController(IBrandService brand,IImageService imageService, IWebHostEnvironment webHostEnvironment)
        {
            _brand = brand;
            _imageService = imageService;
            _webHostEnvironment = webHostEnvironment;

        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Upload-Image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded or file is empty.");
                }

                _imageService.ValidateFileUpload(file);

                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var fileExtension = Path.GetExtension(file.FileName).ToLower();
                var localPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "Brand", $"{fileName}{fileExtension}");

                using (var stream = new FileStream(localPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var uploadedImage = await _imageService.SaveImageBrandToDatabaseAsync(fileName, fileExtension);

                return Ok(uploadedImage);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("get-all-brand")]
        public async Task<IActionResult> GetAllBrand()
        {
            var brands = await _brand.GetAllAsync();
            var brandVm = new List<BrandVm>();
            foreach (var brand in brands)
            {
                brandVm.Add(new BrandVm
                {
                    BrandId = brand.BrandId,
                    BrandName = brand.BrandName,
                    BrandImage = brand.BrandImage,
                    Description = brand.Description
                });
            }
            return Ok(brandVm);
        }


        [HttpGet("Get-All-Brand-Pagination")]
        public async Task<IActionResult> GetAllBrandPagination([FromQuery] int pageIndex, int pageSize)
        {
            if (pageIndex <= 0 || pageSize <= 0)
            {
                return BadRequest(new { message = "PageIndex and PageSize must be greater than 0." });
            }

            var brands = await _brand.GetBrandListPaginationAsync(pageIndex, pageSize);
            return Ok(brands);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("get-brand-by-id/{id}")]
        public async Task<IActionResult> GetBrandById(Guid id)
        {
            var brand = await _brand.GetByIdAsync(id);
            if (brand != null)
            {
                var brandVm = new BrandVm()
                {
                    BrandId = brand.BrandId,
                    BrandName = brand.BrandName,
                    BrandImage = brand.BrandImage,
                    Description = brand.Description
                };
                return Ok(brandVm);
            }
            return NotFound("The brand does not exist!");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add-new-brand")]
        public async Task<IActionResult> AddNewBrand([FromBody] AddBrandVm addBrandVm)
        {
            var brand = new Brand()
            {
                BrandId = Guid.NewGuid(),
                BrandName = addBrandVm.BrandName,
                BrandImage = addBrandVm.BrandImage,
                Description = addBrandVm.Description
            };
            await _brand.AddAsync(brand);
            return Ok(brand);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-brand/{id}")]
        public async Task<IActionResult> UpdateBrand(Guid id, [FromBody] AddBrandVm addBrandVm)
        {
            var brand = await _brand.GetByIdAsync(id);
            if (brand != null)
            {
                brand.BrandName = addBrandVm.BrandName;
                brand.BrandImage = addBrandVm.BrandImage;
                brand.Description = addBrandVm.Description;

                await _brand.UpdateAsync(brand);
                return Ok(brand);
            }
            return BadRequest("The brand does not exist!");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-brand/{id}")]
        public async Task<IActionResult> DeleteBrand(Guid id)
        {
            var brand = await _brand.GetByIdAsync(id);
            if (brand != null)
            {
                await _brand.DeleteAsync(brand);
                return Ok(brand);
            }
            return BadRequest("Delete Faild!");
        }
    }
}
