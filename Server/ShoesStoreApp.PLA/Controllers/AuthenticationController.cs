using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ShoesStoreApp.BLL.Services.AuthenticationService;
using ShoesStoreApp.BLL.Services.Image;
using ShoesStoreApp.BLL.ViewModels.Auth;
using System.Security.Claims;

namespace ShoesStoreApp.PLA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IImageService _imageService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuthenticationController(IAuthenticationService authenticationService, IImageService imageService, IWebHostEnvironment webHostEnvironment)
        {
            _authenticationService = authenticationService;
            _imageService = imageService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterVm registerVm)
        {
            try
            {
                var result = await _authenticationService.RegisterUserAsync(registerVm);
                return Created(nameof(Register), result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginVm loginVm)
        {
            try
            {
                var result = await _authenticationService.LoginUserAsync(loginVm);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            try
            {
                var result = await _authenticationService.RefreshTokenAsync(refreshToken);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [Authorize]
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
                var localPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "Avatar", $"{fileName}{fileExtension}");

                using (var stream = new FileStream(localPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var uploadedImage = await _imageService.SaveAvatarToDatabaseAsync(fileName, fileExtension);

                return Ok(uploadedImage);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("user-info")]
        public async Task<IActionResult> GetUserInfo()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { Message = "User is not authenticated." });
                }

                var userInfo = await _authenticationService.GetUserInfoAsync(Guid.Parse(userId));
                return Ok(userInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("update-user-info")]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserVm updateUserVm)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { Message = "User is not authenticated." });
                }

                var isUpdated = await _authenticationService.UpdateUserInfoAsync(Guid.Parse(userId), updateUserVm);
                if (!isUpdated)
                {
                    return BadRequest(new { Message = "Failed to update user information." });
                }

                return Ok(new { Message = "User information updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("Change-Password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordVm passwordVm)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { Message = "User is not authenticated." });
                }

                var isUpdated = await _authenticationService.ChangePasswordAsync(Guid.Parse(userId), passwordVm);
                if (!isUpdated)
                {
                    return BadRequest(new { Message = "Failed to update user information." });
                }

                return Ok(new { Message = "User information updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
