using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesStoreApp.BLL.Services.Custumer;
using ShoesStoreApp.BLL.ViewModels;

namespace ShoesStoreApp.PLA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get-all-user")]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await _userService.getAllUsersAsync();
            var userVm = new List<UserVM>();
            foreach (var user in users)
            {
                var roleId = await _userService.getRoleIdByName("User");
                if (user.RoleId == roleId)
                {
                    userVm.Add(new UserVM
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        Email = user.Email,
                        Address = user.Address,
                        Status = user.Status,
                    });
                }
            }
            return Ok(userVm);
        }

        [HttpGet("get-user-by-id/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            var userVm = new UserVM()
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Address = user.Address,
                Status = user.Status
            };
            return Ok(userVm);
        }

        [HttpPut("update-user-status/{id}")]
        public async Task<IActionResult> UpdateUserStatus(Guid id, [FromBody] UserVM userVM)
        {
            var user = await _userService.getUserById(id);
            if (user != null)
            {
                if(user.Status == false)
                {
                    user.Status = true;
                }
                else
                {
                    user.Status = false;
                }
                await _userService.UpdateAsync(user);
                return Ok(user);
            }
            return BadRequest("Update status faild");
        }
    }
}
