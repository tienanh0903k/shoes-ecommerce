using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Infrastructure;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.Custumer;

public class UserService : BaseService<User>, IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public UserService(UserManager<User> userManager, RoleManager<Role> roleManager, IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    public async Task<UserVM> getUserByEmail(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        
        return new UserVM()
        {
            Id = user.Id,
            FullName = user.FullName,
            Address = user.Address,
            Email = user.Email,
        };
    }

    public async Task<List<User>> getAllUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        return users;
    }

    public async Task<Guid> getRoleIdByName(string name)
    {
        var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == name);
        var roleId = role.Id;
        return roleId;
    }

    //public async Task<User> updateStatus(Guid id)
    //{
    //    var user = await _userManager.Users.FirstOrDefaultAsync(r => r.Id == id);
    //    if (user.Status == false)
    //    {
    //        user.Status = true;
    //    }
    //    else
    //    {
    //        user.Status = false;
    //    }
    //    return user;
    //}

    public Task<User> getUserById(Guid id)
    {
        return  _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
    }
}