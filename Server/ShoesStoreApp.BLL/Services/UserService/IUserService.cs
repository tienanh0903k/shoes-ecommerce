using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.Custumer;

public interface IUserService : IBaseService<User>
{
    Task<UserVM> getUserByEmail(string id);
    Task<List<User>> getAllUsersAsync();
    Task<Guid> getRoleIdByName(string name);
    Task<User> getUserById(Guid id);
    //Task<User> updateStatus(Guid id);
    
}