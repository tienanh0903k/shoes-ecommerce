using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesStoreApp.BLL.Services.SizeService;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.PLA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ISizeSrevice _sizeService;
        public SizeController(ISizeSrevice sizeService)
        {
            _sizeService = sizeService;
        }

        [HttpGet("get-all-size")]
        public async Task<IActionResult> GetAllSize()
        {
            var sizes = await _sizeService.GetAllAsync();
            var sizeVm = new List<SizeVm>();
            foreach (var size in sizes)
            {
                sizeVm.Add(new SizeVm
                {
                    SizeId = size.SizeId,
                    SizeName = size.SizeName,
                    Quantity = size.Quantity,
                    Status = size.Status,
                    ProductId = size.ProductId,
                });
            }
            return Ok(sizeVm);
        }

        [HttpGet("get-size-by-id/{id}")]
        public async Task<IActionResult> GetSizeById(Guid id)
        {
            var size = await _sizeService.GetByIdAsync(id);
            if(size != null)
            {
                var sizeVm = new SizeVm()
                {
                    SizeId = size.SizeId,
                    SizeName = size.SizeName,
                    Quantity = size.Quantity,
                    Status = size.Status,
                    ProductId = size.ProductId,
                };
                return Ok(sizeVm);
            }
            return BadRequest("The size does not exist!");
        }


        [HttpGet("get-size-by-product/{id}")]
        public async Task<IActionResult> GetSizeByProductId(Guid id)
        {
            var sizes = await _sizeService.GetSizeByProductId(id);
            var sizeVm = new List<SizeVm>();
            if (sizes != null)
            {
                foreach (var size in sizes)
                {
                    sizeVm.Add(new SizeVm
                    {
                        SizeId = size.SizeId,
                        SizeName = size.SizeName,
                        Quantity = size.Quantity,
                        Status = size.Status,
                        ProductId = size.ProductId,
                    });
                }
                return Ok(sizeVm);
            }
            return BadRequest("The size does not exist!");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add-new-size")]
        public async Task<IActionResult> AddNewSize([FromBody] AddSizeVm addSizeVm)
        {
            var size = new Size()
            {
                SizeId = Guid.NewGuid(),
                SizeName = addSizeVm.SizeName,
                Quantity = addSizeVm.Quantity,
                Status = addSizeVm.Status,
                ProductId = addSizeVm.ProductId,
            };
            await _sizeService.AddAsync(size);
            return Ok(size);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-size/{id}")]
        public async Task<IActionResult> UpdateSize(Guid id, [FromBody] AddSizeVm addSizeVm)
        {
            var size = await _sizeService.GetByIdAsync(id);
            if(size != null)
            {
                size.SizeName = addSizeVm.SizeName;
                size.Quantity = addSizeVm.Quantity;
                size.Status = addSizeVm.Status;
                size.ProductId = addSizeVm.ProductId;

                await _sizeService.UpdateAsync(size);
                return Ok(size);
            }
            return BadRequest("Update Faild!");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-status-size/{id}")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateStatusSizeVm updateStatusSizeVm)
        {
            var size = await _sizeService.GetByIdAsync(id);
            if(size != null)
            {
                size.Status = updateStatusSizeVm.Status;

                await _sizeService.UpdateAsync(size);
                return Ok(size);
            }
            return BadRequest("Update status size faild!");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-size/{id}")]
        public async Task<IActionResult> DeleteSize(Guid id)
        {
            var size = await _sizeService.GetByIdAsync(id);
            if(size != null)
            {
                await _sizeService.DeleteAsync(size);
                return Ok(size);
            }
            return BadRequest("Delete Faild");
        }
    }
}
